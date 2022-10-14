using AutoMapper;
using ContactListWebService.Entities;
using ContactListWebService.Models;
using ContactListWebService.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactListWebService.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IMapper _mapper;
        private readonly IDataProtector _dataProtector;

        public UsersController(IUserInfoRepository userInfoRepository, IMapper mapper, IDataProtectionProvider provider)
        {
            _userInfoRepository = userInfoRepository ??
                throw new ArgumentNullException(nameof(userInfoRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _dataProtector = provider.CreateProtector("UsersController");
        }

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<UsersDto>>> GetUsers()
        {
            var usersEntities = await _userInfoRepository.GetUsersAsync();
            return Ok(_mapper.Map<IEnumerable<UsersDto>>(usersEntities));
        }

        [HttpGet("users/{userId}")]
        public async Task<ActionResult<UserDto>> GetUser(int userId)
        {
            var userEntity = await _userInfoRepository.GetUserAsync(userId);
            if (userEntity == null)
            {
                return NotFound($"User with id = {userId} does not exist");
            }
            return Ok(_mapper.Map<UserDto>(userEntity));
        }

        [HttpGet("authorize/users")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersExt()
        {
            var usersEntities = await _userInfoRepository.GetUsersAsync();
            foreach (var user in usersEntities)
            {
                user.Password = _dataProtector.Unprotect(user.Password);
            }
            return Ok(_mapper.Map<IEnumerable<ExtUserDto>>(usersEntities));
        }

        [HttpGet("authorize/users/{userId}")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUserExt(int userId)
        {
            var userEntity = await _userInfoRepository.GetUserAsync(userId);
            if (userEntity == null)
            {
                return NotFound($"User with id = {userId} does not exist");
            }
            userEntity.Password = _dataProtector.Unprotect(userEntity.Password);
            return Ok(_mapper.Map<ExtUserDto>(userEntity));
        }

        [HttpPost("authorize/users")]
        [Authorize]
        public async Task<ActionResult> AddUser(UserForCreationDto user)
        {
            if (await _userInfoRepository.HasUserExistAsync(user.Email))
            {
                return BadRequest("User exist in database");
            }
            user.Password = _dataProtector.Protect(user.Password);

            var addedUser = await _userInfoRepository.AddUserAsync(_mapper.Map<User>(user));

            return Created("users", null);
        }

        [HttpDelete("authorize/users/{userId}")]
        [Authorize]
        public async Task<ActionResult> RemoveUser(int userId)
        {
            var _user = await _userInfoRepository.GetUserAsync(userId);
            if (_user == null)
            {
                return NotFound("User does not exist");
            }
            await _userInfoRepository.RemoveUser(_user);
            return NoContent();
        }

        [HttpPut("authorize/users/{userId}")]
        [Authorize]
        public async Task<ActionResult> UpdateUser(int userId, UserForCreationDto user)
        {
            var userEntity = await _userInfoRepository.GetUserAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            _mapper.Map(user, userEntity);
            await _userInfoRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("authorize/users/{userid}")]
        [Authorize]
        public async Task<ActionResult> PartiallyPathUser(int userId, JsonPatchDocument<UserForCreationDto> patchDocument)
        {
            var userEntity = await _userInfoRepository.GetUserAsync(userId);
            if (userEntity == null)
            {
                return NotFound();
            }
            IEnumerable<User> usersEntities;
            if (patchDocument.Operations.Any(op => op.path == "/email"))
            {
                usersEntities = await _userInfoRepository.GetUsersAsync();
                foreach (var op in patchDocument.Operations)
                {
                    if (usersEntities.Any(user => user.Email == (string)op.value))
                    {
                        return BadRequest("Email is used by current or other user");
                    }
                }
            }

            var userToPatch = _mapper.Map<UserForCreationDto>(userEntity);

            patchDocument.ApplyTo(userToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!TryValidateModel(userToPatch))
            {
                return BadRequest();
            }

            _mapper.Map(userToPatch, userEntity);
            await _userInfoRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}
