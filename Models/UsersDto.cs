using System.ComponentModel.DataAnnotations;

namespace ContactListWebService.Models
{
    public class UsersDto
    {
        
        public long UserId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;
    }
}
