namespace GestaHogar.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Measure {  get; set; }
        public EUnit Unit { get; set; }


        public enum EUnit
        {
            Grams,
            Liter,
            Package
        }
    }
}
