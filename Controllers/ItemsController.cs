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

        // HOME PAGE
        public IActionResult Index()
        {
            return View();
        }

        // ITEMS FOR SALE PAGE
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ItemsForSale(string searchString, string? ErrorMessage)
        {

            // GET ALL ITEMS FROM DATABASE
            var items = from i in itemsDbContext.Item
                         select i;
            var cart = _cartService.Get();

            // CHECK IF SEARCH STRING IS NOT NULL OR EMPTY
            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Name!.Contains(searchString));
            }

            // CREATE VIEW MODEL
            var ItemVM = new ViewItems
            {
                Items = await items.ToListAsync()
            };

            // ADD ERROR MESSAGE TO VIEW BAG
            ViewBag.Error = ErrorMessage;

            return View(ItemVM);
        }

        // METHOD TO BUY ITEMS
        public async Task<IActionResult> Buy(Guid id)
        {
            // GET BUYER ID
            var product = await itemsDbContext.Item.FindAsync(id);

            // CHECK IF ITEM EXISTS AND ADD TO CART
            if (product != null)
            {
                _cartService.Add(product, 1);
            }
            
            // REDIRECT TO ITEMS FOR SALE PAGE
            return RedirectToAction("ItemsForSale", new { ErrorMessage = "Item added to Cart." });
        }


        // ACCOUNT ITEMS PAGE (USER'S ITEMS)
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AccountItems()
        {

            // GET ALL ITEMS AND PURCHASES FROM DATABASE
            var items = await itemsDbContext.Item.ToListAsync();
            var purchase = await itemsDbContext.Purchases.ToListAsync();

            // GET USER ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // SELECT STATEMENT TO GET TOP BUYERS
            var topBuyers = purchase
                .Where(p => p.BuyerId != userId && p.SellerId == userId)
                .GroupBy(p => p.BuyerId)
                .Select(g => new TopBuyer
                {
                    BuyerId = g.Key,
                    TotalSpent = g.Sum(p => p.Cost)
                })
                .OrderByDescending(b => b.TotalSpent)
                .Take(5) // For example, top 5 buyers
                .ToList();

            // CREATE VIEW MODEL
            var viewModel = new AccountItemsModel
            {
                Items = items,
                Purchase = purchase,
                TopBuyers = topBuyers
            };

            return View(viewModel);
        }

        // SUCCESS PAGE AFTER CREATING ITEM
        public IActionResult CreateSuccess()
        {
            return View();
        }

        // CREATE ITEM PAGE
        [Authorize]
        [HttpGet]
        public IActionResult CreateItem()
        {
            return View();
        }

        // CREATE ITEM METHOD
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateItem(AddItem addItem)
        {

            // GET USER ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // CREATE ITEM OBJECT AND ADD TO DATABASE
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

            // CONVERT PICTURE TO BASE64 STRING
            using (var memoryStream = new MemoryStream())
            {
                addItem.Picture.CopyTo(memoryStream);
                var imageBytes = memoryStream.ToArray();
                item.Picture = Convert.ToBase64String(imageBytes);
            }

            // ADD ITEM TO DATABASE
            await itemsDbContext.Item.AddAsync(item);
            await itemsDbContext.SaveChangesAsync();
            return RedirectToAction("CreateSuccess");
        }

        // ITEM VIEW PAGE
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ItemView(Guid id)
        {

            // GET ITEM FROM DATABASE
            var itemView = await itemsDbContext.Item.FirstOrDefaultAsync(x => x.Id == id);

            // CREATE VIEW MODEL
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
            // GET ITEM FROM DATABASE
            var itemEdit = await itemsDbContext.Item.FirstOrDefaultAsync(x => x.Id == id);

            // CREATE VIEW MODEL
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

        // EDIT ITEM METHOD
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditItem updateItem)
        {
            // GET USER ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // GET ITEM FROM DATABASE
            var itemEdit = await itemsDbContext.Item.FindAsync(updateItem.Id);

            // UPDATE ITEM IN DATABASE
            if (itemEdit != null)
            {
                itemEdit.Id = updateItem.Id;
                itemEdit.UserId = updateItem.UserId;
                itemEdit.Name = updateItem.Name;
                itemEdit.Description = updateItem.Description;
                itemEdit.Price = updateItem.Price;
                itemEdit.Quantity = updateItem.Quantity;

                // SAVE CHANGES
                await itemsDbContext.SaveChangesAsync();

                if (User.IsInRole("Administrator"))
                {
                    return RedirectToAction("ItemsForSale");
                }

                return RedirectToAction("AccountItems");
            }

            if (User.IsInRole("Administrator"))
            {
                return RedirectToAction("ItemsForSale");
            }

            return RedirectToAction("AccountItems");
        }

        // Delete the item from the edit page
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(EditItem deleteItem)
        {

            // GET ITEM FROM DATABASE
            var itemDelete = await itemsDbContext.Item.FindAsync(deleteItem.Id);

            // DELETE ITEM FROM DATABASE
            if (itemDelete != null)
            {

                // DELETE ITEM FROM DATABASE AND SAVE CHANGES
                itemsDbContext.Item.Remove(itemDelete);
                await itemsDbContext.SaveChangesAsync();

                //REDIRECT TO ITEMS PAGE IF ADMIN, ELSE REDIRECT TO ACCOUNT ITEMS PAGE
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("ItemsForSale");
                }

                return RedirectToAction("AccountItems");
            }

            return RedirectToAction("AccountItems");

        }

        // REFERENCE PAGE
        public IActionResult References()
        {
            return View();
        }

    }
}
