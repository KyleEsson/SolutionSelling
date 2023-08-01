using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolutionSelling.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using SolutionSelling.Areas.Items.Data;
using Microsoft.EntityFrameworkCore;

namespace SolutionSelling.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ItemsDbContext itemsDbContext;

        public ItemsController(ItemsDbContext itemsDbContext)
        {
            this.itemsDbContext = itemsDbContext;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ItemsForSale()
        {
            var itemsview = await itemsDbContext.Item.ToListAsync();
            return View(itemsview);
        }

        public IActionResult CreateSuccess()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateItem()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateItem(AddItem addItem)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var item = new Item()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Name = addItem.Name,
                Description = addItem.Description,
                Price = addItem.Price,
                Quantity = addItem.Quantity
            };

            await itemsDbContext.Item.AddAsync(item);
            await itemsDbContext.SaveChangesAsync();
            return RedirectToAction("CreateSuccess");
        }

    }
}
