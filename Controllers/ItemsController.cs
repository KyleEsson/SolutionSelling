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
    public class ItemsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ItemsDbContext itemsDbContext;
        private readonly CartService _cartService;

        public ItemsController(ILogger<HomeController> logger, ItemsDbContext itemsDbContext, CartService cartService)
        {
            this.itemsDbContext = itemsDbContext;
            _cartService = cartService;
            _logger = logger;
        }

        // ITEMS FOR SALE PAGE
        [HttpGet]
        public async Task<IActionResult> ItemsForSale(string searchString, string? ErrorMessage)
        {
            var items = from i in itemsDbContext.Item
                         select i;
            var cart = _cartService.Get();

            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Name!.Contains(searchString));
            }

            var ItemVM = new ViewItems
            {
                Items = await items.ToListAsync()
            };

            // Add the message to the ModelState if need be
            ViewBag.Error = ErrorMessage;

            return View(ItemVM);
        }

        public async Task<IActionResult> Buy(Guid id)
        {
            var product = await itemsDbContext.Item.FindAsync(id);
            if (product != null)
            {
                _cartService.Add(product, 1);
            }
            return RedirectToAction("ItemsForSale", new { ErrorMessage = "Item added to Cart." });
        }


        // ACCOUNT ITEMS PAGE (USER'S ITEMS)
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AccountItems()
        {
            var items = await itemsDbContext.Item.ToListAsync();
            var purchase = await itemsDbContext.Purchases.ToListAsync();

            var viewModel = new AccountItemsModel
            {
                Items = items,
                Purchase = purchase
            };

            return View(viewModel);
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
                Quantity = addItem.Quantity,
                PictureFormat = addItem.Picture.ContentType
            };

            using (var memoryStream = new MemoryStream())
            {
                addItem.Picture.CopyTo(memoryStream);
                var imageBytes = memoryStream.ToArray();
                item.Picture = Convert.ToBase64String(imageBytes);
            }

            await itemsDbContext.Item.AddAsync(item);
            await itemsDbContext.SaveChangesAsync();
            return RedirectToAction("CreateSuccess");
        }

        // ITEM VIEW PAGE
        [HttpGet]
        public async Task<IActionResult> ItemView(Guid id)
        {
            var itemView = await itemsDbContext.Item.FirstOrDefaultAsync(x => x.Id == id);

            var viewModel = new ItemView { Name = itemView.Name };
            viewModel.Picture = itemView.Picture;
            viewModel.PictureFormat = itemView.PictureFormat;
            viewModel.Description = itemView.Description;
            viewModel.Name = itemView.Name;

            return View(viewModel);
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

        // Delete the item from the edit page
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(EditItem deleteItem)
        {
            var itemDelete = await itemsDbContext.Item.FindAsync(deleteItem.Id);

            if (itemDelete != null)
            {
                itemsDbContext.Item.Remove(itemDelete);
                await itemsDbContext.SaveChangesAsync();

                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("ItemsForSale");
                }

                return RedirectToAction("AccountItems");
            }

            return RedirectToAction("AccountItems");

        }

        public IActionResult References()
        {
            return View();
        }

    }
}
