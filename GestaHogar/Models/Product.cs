using GestaHogar.Util;

namespace GestaHogar.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Category { get; set; }
        public UDouble Amount { get; set; }
        public EUnit Unit { get; set; }
        public List<User> Users { get; set; } = [];
    }
}
