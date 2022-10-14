using ContactListWebService.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ContactListWebService.Models
{
    public class CategoryForCreationDto
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Required]
        public Category.CategoryEnum CategoryType { get; set; }

        [StringLength(20)]
        public string Subcategory { get; set; } = string.Empty;
    }
}
