using ContactListWebService.Repositories;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ContactListWebService.Controllers
{
    [Route("authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IDataProtector _dataProtector;

        public class AuthenticationRequest
        {
            public string? Email { get; set; }
            public string? Password { get; set; }
        }

        public class AuthUser
        {
            public long UserId { get; set; }

            public string Name { get; set; } = string.Empty;

            public string Surname { get; set; } = string.Empty;

            public AuthUser(int userId, string userName, string surname)
            {
                UserId = userId;
                Name = userName;
                Surname = surname;
            }
        }

        public AuthenticationController(IConfiguration configuration, IUserInfoRepository userInfoRepository, IDataProtectionProvider provider)
        {
            _userInfoRepository = userInfoRepository ??
                throw new ArgumentNullException(nameof(userInfoRepository));
            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
            _dataProtector = provider.CreateProtector("UsersController");
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequest request)
        {
            // User validation
            var user = ValidateUserCredentials(request.Email, request.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            // Create token
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.UserId.ToString()));
            claimsForToken.Add(new Claim("given_name", user.Name));
            claimsForToken.Add(new Claim("family_name", user.Surname));

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
               .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        public AuthUser? ValidateUserCredentials(string? email, string? password)
        {
            var users = _userInfoRepository.GetUsersAsync().Result;
            var user = users.FirstOrDefault(user => user.Email == email);
            if (user == null)
            {
                return null;
            }
            var userPassword = _dataProtector.Unprotect(user.Password);
            if (password == userPassword)
            {
                return new AuthUser(user.UserId, user.Name, user.Surname);
            }
            else
            {
                return null;
            }
        }
    }
}
