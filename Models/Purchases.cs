using System.ComponentModel.DataAnnotations;

namespace SolutionSelling.Models
{
    public class Purchases
    {
        public Guid Id { get; set; }

        public string SellerId { get; set; }

        public string BuyerId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        public int Quantity { get; set; }

        [Required]
        public string? Picture { get; set; }

        [Required]
        public string? PictureFormat { get; set; }

        public DateTime PurchaseDate { get; set; }
    }
}
