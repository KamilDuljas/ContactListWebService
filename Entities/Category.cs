using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ContactListWebService.Entities
{
    public class Category
    {
        public enum CategoryEnum
        {
            Business,
            Private,
            Other
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CategoryEnum CategoryType { get; set; }

        public string Subcategory { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public int UserId { get; set; }
    }
}
