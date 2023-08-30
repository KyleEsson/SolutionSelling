using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolutionSelling.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using SolutionSelling.Areas.Items.Data;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SolutionSelling.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly ItemsDbContext _context;
        public CartController(CartService cartService, ItemsDbContext context)
        {
            _cartService = cartService;
            _context = context;
        }

        //ADD METHOD TO SHOW CART
        [Authorize]
        public IActionResult Index(string errorMessage = null)
        {
            var cart = _cartService.Get();

            // CHECK IF ERROR MESSAGE IS NOT NULL OR EMPTY
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewBag.Error = errorMessage;
            }

            return View(cart);
        }

        // ADD METHOD TO REMOVE ITEMS FROM CART
        public IActionResult Remove(string id)
        {
            _cartService.Remove(id);
            return RedirectToAction("Index");
        }

        public IActionResult Purchase()
        {
            // GET BUYER ID
            var buyerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // GET CART
            var cart = _cartService.Get();

            //LOOP THROUGH CART ITEMS
            foreach (var cartItem in cart.CartItems)
            {
                // GET ORIGINAL ITEM FROM DATABASE
                var originalItem = _context.Item.FirstOrDefault(i => i.Id == cartItem.Item.Id);

                // CHECK IF ITEM EXISTS
                if (originalItem != null) 
                {
                    // ADJUST QUANTITY IN STOCK
                    originalItem.Quantity -= cartItem.Quantity;

                    // CHECK IF QUANTITY IS LESS THAN 0
                    if (originalItem.Quantity < 0)
                    {
                        // IF PURCHASE QUANTITY IS GREATER THAN QUANTITY IN STOCK, RETURN TO CART WITH ERROR MESSAGE
                        return RedirectToAction("Index", new { ErrorMessage = "Insufficient stock for " + originalItem.Name });
                    }

                    // CREATE PURCHASE OBJECT AND ADD TO DATABASE
                    var purchase = new Purchases
                    {
                        Id = Guid.NewGuid(),
                        SellerId = originalItem.UserId,
                        BuyerId = buyerId,
                        Name = originalItem.Name,
                        Description = originalItem.Description,
                        Cost = cartItem.Cost,
                        Quantity = cartItem.Quantity,
                        Picture = originalItem.Picture,
                        PictureFormat = originalItem.PictureFormat,
                        PurchaseDate = DateTime.Now
                    };

                    _context.Purchases.Add(purchase);
                }
                else
                {
                    // PLACEHOLDER FOR ERROR HANDLING
                }
            }

            // SAVE CHANGES TO DATABASE
            _context.SaveChanges();

            // CLEAR CART AFTER PURCHASE
            _cartService.Clear();

            // REDIRECT TO ACCOUNT ITEMS
            return RedirectToAction("AccountItems", "Items");
        }

    }
}
