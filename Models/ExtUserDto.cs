using System.Text.Json.Serialization;

namespace ContactListWebService.Models
{
    public class ExtUserDto
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CategoryDto UserCategory { get; set; }

        public string Phone { get; set; }

        public string BirthDate { get; set; }
    }
}
