using ContactListWebService.Entities;
using System.Text.Json.Serialization;

namespace ContactListWebService.Models
{
    public class CategoryDto
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Category.CategoryEnum CategoryType { get; set; }

        public string Subcategory { get; set; } = string.Empty;
    }
}
