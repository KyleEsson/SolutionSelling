namespace SolutionSelling.Models
{
    public class CartItem
    {
        public string Id { get; set; }
        public Item Item { get; set; }
        public decimal Cost
        {
            get { return (Item.Price) * Quantity; }
        }
        public int Quantity { get; set; }
    }
}
