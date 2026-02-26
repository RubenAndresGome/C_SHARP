using Azure.Core.Pipeline;

namespace Backend.DTOs
{
    public class BeerDto
    {
        public long Id { get; set; }
        public string Name { get; set; }    
        public long BrandID { get; set; }
        public decimal Alcohol { get; set; }


    }
}
