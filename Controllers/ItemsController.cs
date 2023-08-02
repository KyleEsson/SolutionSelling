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

        // ITEMS FOR SALE PAGE
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ItemsForSale()
        {
            var itemsview = await itemsDbContext.Item.ToListAsync();
            return View(itemsview);
        }

        // ACCOUNT ITEMS PAGE (USER'S ITEMS)
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AccountItems()
        {
            var itemsview = await itemsDbContext.Item.ToListAsync();
            return View(itemsview);
        }

        // ADD NEW ITEMS PAGE
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

        // EDIT ITEMS PAGE
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var itemEdit = await itemsDbContext.Item.FirstOrDefaultAsync(x => x.Id == id);

            if (itemEdit != null)
            {
                var editItem = new EditItem()
                {
                    Id = itemEdit.Id,
                    UserId = itemEdit.UserId,
                    Name = itemEdit.Name,
                    Description = itemEdit.Description,
                    Price = itemEdit.Price,
                    Quantity = itemEdit.Quantity
                };

                return View(editItem);
            }


            return RedirectToAction("AccountItems");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditItem updateItem)
        {
            var itemEdit = await itemsDbContext.Item.FindAsync(updateItem.Id);

            if (itemEdit != null)
            {
                itemEdit.Id = updateItem.Id;
                itemEdit.UserId = updateItem.UserId;
                itemEdit.Name = updateItem.Name;
                itemEdit.Description = updateItem.Description;
                itemEdit.Price = updateItem.Price;
                itemEdit.Quantity = updateItem.Quantity;

                await itemsDbContext.SaveChangesAsync();

                return RedirectToAction("AccountItems");
            }

            return RedirectToAction("AccountItems");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(EditItem deleteItem)
        {
            var itemDelete = await itemsDbContext.Item.FindAsync(deleteItem.Id);

            if (itemDelete != null)
            {
                itemsDbContext.Item.Remove(itemDelete);
                await itemsDbContext.SaveChangesAsync();

                return RedirectToAction("AccountItems");
            }

            return RedirectToAction("AccountItems");

        }
    }
}
