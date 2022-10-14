using System.ComponentModel.DataAnnotations;

namespace ContactListWebService.Models
{
    public class UserForCreationDto
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        public CategoryForCreationDto UserCategory { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string BirthDate { get; set; }
    }
}
