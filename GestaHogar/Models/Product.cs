namespace GestaHogar.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Measure { get; set; }
        public EUnit Unit { get; set; }
    }
}
