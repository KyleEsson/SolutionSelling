namespace SolutionSelling.Models
{
    public class CartService : ICartService
    {
        private Cart _cart = new Cart();


        // ADD METHOD TO GET CART
        public Cart Get()
        {
            return _cart;
        }

        // ADD METHOD TO ADD ITEMS TO CART
        public void Add(Item product, int quantity)
        {
            Cart cart = Get();
            // GET CARTITEM FOR PRODUCT
            var item = cart.CartItems.FirstOrDefault(i => i.Item.Id == product.Id);
            // IF CARTITEM DOES NOT EXIST, CREATE IT
            if (item == null)
            {
                item = new CartItem { Item = product, Quantity = quantity };
                cart.CartItems.Add(item);
            }
            // OTHERWISE, INCREMENT QUANTITY
            else
            {
                item.Quantity += quantity;
            }
        }


        // ADD METHOD TO REMOVE ITEMS FROM CART
        public void Remove(string productIdAsString)
        {
            Guid productId;

            // TRY TO PARSE THE INPUT STRING AS A GUID
            if (Guid.TryParse(productIdAsString, out productId))
            {
                var cart = Get();
                var itemToRemove = cart.CartItems.FirstOrDefault(item => item.Item.Id == productId);

                // IF THE ITEM EXISTS, REMOVE IT
                if (itemToRemove != null)
                {
                    cart.CartItems.Remove(itemToRemove);
                }
            }
        }


        // ADD METHOD TO CLEAR CART
        public void Clear()
        {
            var cart = Get();
            cart.CartItems.Clear();
        }
    }
}
