namespace SolutionSelling.Models
{
    public class Cart
    {
        public string Id { get; set; }
        public List<CartItem> CartItems { get; set; }
        public decimal TotalCost => CartItems.Sum(item => item.Cost);
        public Cart()
        {
            CartItems = new List<CartItem>();
        }
    }
}
