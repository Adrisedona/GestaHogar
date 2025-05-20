using System.Text.Json.Serialization;
using GestaHogar.Util;

namespace GestaHogar.Models
{
    public class Product
    {
        public int? Id { get; set; }
        public required string Name { get; set; }
        public required string Category { get; set; }
        public UFloat Amount { get; set; }
        public EUnit Unit { get; set; }

        [JsonIgnore]
        public List<User> Users { get; set; } = [];
    }
}
