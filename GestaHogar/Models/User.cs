using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace GestaHogar.Models
{
    public class User : IdentityUser
    {
        [JsonIgnore]
        public List<Product> Products { get; set; } = [];
    }
}
