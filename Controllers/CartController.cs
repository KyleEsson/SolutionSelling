using Microsoft.AspNetCore.Mvc;
using SolutionSelling.Areas.Items.Data;
using SolutionSelling.Models;

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
    }
}
