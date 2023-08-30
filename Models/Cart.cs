namespace SolutionSelling.Models
{
    public class Cart
    {
        public string Id { get; set; }

        // CREATE LIST OF CARTITEMS
        public List<CartItem> CartItems { get; set; }

        // CREATE TOTALCOST PROPERTY
        public decimal TotalCost => CartItems.Sum(item => item.Cost);

        // CREATE CONSTRUCTOR
        public Cart()
        {
            CartItems = new List<CartItem>();
        }
    }
}
