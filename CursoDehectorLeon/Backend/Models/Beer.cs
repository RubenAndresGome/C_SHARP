using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Beer
    {
        public long BeerID { get; set; }
        public string Name { get; set; } = string.Empty;

        public long BrandID { get; set; }

        [ForeignKey("BrandID")]
        public virtual Brand Brand { get; set; }

        [Column(TypeName ="decimal(18,2)")]
        public decimal Alcohol { get; set; }

    }
}
