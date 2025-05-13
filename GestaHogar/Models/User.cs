using Microsoft.AspNetCore.Identity;

namespace GestaHogar.Models
{
    public class User : IdentityUser
    {
        public List<Product> Products { get; set; } = [];
    }
}
