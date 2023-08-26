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
        [Authorize]
        public IActionResult Index()
        {
            var cart = _cartService.Get();
            return View(cart);
        }

        public IActionResult Remove(string id)
        {
            _cartService.Remove(id);
            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            _cartService.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Purchase()
        {
            var buyerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = _cartService.Get();
            foreach (var cartItem in cart.CartItems)
            {
                var originalItem = _context.Item.FirstOrDefault(i => i.Id == cartItem.Item.Id);

                if (originalItem != null)  // ensure that the item exists in the database
                {
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
                    // Handle the case where the item doesn't exist in the database (optional)
                }
            }

            _context.SaveChanges();
            _cartService.Clear();
            return RedirectToAction("AccountItems", "Items");
        }   
    }
}
