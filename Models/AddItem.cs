using System.ComponentModel.DataAnnotations;

namespace SolutionSelling.Models
{
    public class AddItem
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [Required]
        public IFormFile? Picture { get; set; }
    }
}
