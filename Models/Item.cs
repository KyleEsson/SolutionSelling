using System.ComponentModel.DataAnnotations;

namespace SolutionSelling.Models
{
    public class Item
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [Required]
        public byte[]? Picture { get; set; }

        [Required]
        public string? PictureFormat { get; set; }
    }
}
