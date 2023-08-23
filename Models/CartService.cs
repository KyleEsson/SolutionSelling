namespace SolutionSelling.Models
{
    public class CartService : ICartService
    {
        private Cart _cart = new Cart();

        public Cart Get()
        {
            return _cart;
        }

        public void Add(Item product, int quantity)
        {
            Cart cart = Get();
            var item = cart.CartItems.FirstOrDefault(i => i.Item.Id == product.Id);
            if (item == null)
            {
                item = new CartItem { Item = product, Quantity = quantity };
                cart.CartItems.Add(item);
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public void Remove(string productIdAsString)
        {
            Guid productId;
            if (Guid.TryParse(productIdAsString, out productId))
            {
                var cart = Get();
                var itemToRemove = cart.CartItems.FirstOrDefault(item => item.Item.Id == productId);
                if (itemToRemove != null)
                {
                    cart.CartItems.Remove(itemToRemove);
                }
            }
            // Optionally, handle the case where the input string is not a valid GUID.
        }

        public void Clear()
        {
            var cart = Get();
            cart.CartItems.Clear();
        }
    }
}
