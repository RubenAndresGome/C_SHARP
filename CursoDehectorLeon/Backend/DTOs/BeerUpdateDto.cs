namespace Backend.DTOs
{
    public class BeerUpdateDto
    {

        public long BeerID { get; set; }
        public string Name { get; set; }
        public long BrandID { get; set; }
        public decimal Alcohol { get; set; }
    }
}
