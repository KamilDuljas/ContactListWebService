namespace ContactListWebService.Models
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public CategoryDto UserCategory { get; set; }

        public string Phone { get; set; }

        public string BirthDate { get; set; }
    }
}
