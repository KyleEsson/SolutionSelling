namespace SolutionSelling.Models
{
    public class EditItem
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Picture { get; set; }

        public string PictureFormat { get; set; }
    }
}
