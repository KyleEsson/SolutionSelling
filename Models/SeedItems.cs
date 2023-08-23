using Microsoft.EntityFrameworkCore;
using SolutionSelling.Areas.Items.Data;
using System.Text;
using SolutionSelling.Helpers;

namespace SolutionSelling.Models
{

    public class SeedItems
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ItemsDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ItemsDbContext>>()))
            {
                // Look for any movies.
                if (context.Item.Any())
                {
                    return;   // DB has been seeded
                }

                byte[] sampleContent = DataHelpers.FromHexString("89504E470D0A1A0A0000000D4948445200000400000003C80806000000470189D40000200049444154789CECBD6DAC6D495ADFF77FAAD6DEE7DC7BBB6F37EDA6339031C2CD40128C261821E238B6456C1C21849C648C250B05E7433C28221882F19861CC6B08B21264F94394489185647FB012DB0A0AC14E267882441834E098");

                context.Item.AddRange(
                    //USER BECS SEEDED ITEMS
                    new Item
                    {
                        Id = Guid.Parse("d7e6f74f-84a5-4e05-ae4b-23bd94c620ff"),
                        UserId = "6e445865-a24d-4543-a6c6-9443d048cdb9",
                        Name = "Broken bottle of beer",
                        Description = "Embrace the beauty of imperfection with our artistically designed broken aesthetic. Each bottle, though unbroken, has the appearance of shattered glass, telling a story of resilience and strength.",
                        Price = 69.69M,
                        Quantity = 0,
                        Picture = sampleContent,
                        PictureFormat = "image/png"
                    },

                    new Item
                    {
                        Id = Guid.Parse("d6e6f74f-84a5-4e05-ae4b-23bd94c620ff"),
                        UserId = "6e445865-a24d-4543-a6c6-9443d048cdb9",
                        Name = "Elegant Vintage Timepiece",
                        Description = "Step back in time with this beautifully crafted wristwatch! With its intricate design and timeless elegance, it's more than just a watch—it's a statement. Perfect for the discerning collector or as a unique gift. Hurry, for time is ticking and this deal won't last long!",
                        Price = 48.2M,
                        Quantity = 3,
                        Picture = sampleContent,
                        PictureFormat = "image/png"
                    },

                    new Item
                    {
                        Id = Guid.Parse("d5e6f74f-84a5-4e05-ae4b-23bd94c620ff"),
                        UserId = "6e445865-a24d-4543-a6c6-9443d048cdb9",
                        Name = "Handmade Artisanal Soap",
                        Description = "Elevate your daily ritual with our all-natural, fragrant soap bars. Infused with essential oils and crafted to perfection, each bar promises a luxurious lathering experience. Rejuvenate, refresh, and redefine clean.",
                        Price = 5.68M,
                        Quantity = 9,
                        Picture = sampleContent,
                        PictureFormat = "image/png"
                    },

                    new Item
                    {
                        Id = Guid.Parse("d4e6f74f-84a5-4e05-ae4b-23bd94c620ff"),
                        UserId = "6e445865-a24d-4543-a6c6-9443d048cdb9",
                        Name = "Eco-Friendly Bamboo Cutlery Set",
                        Description = "Dine with a difference! Our sustainable bamboo cutlery is not just eco-friendly but also sleek and stylish. Portable and perfect for picnics, travels, or daily use. Set your table with an eco-conscious mind!",
                        Price = 106.59M,
                        Quantity = 2,
                        Picture = sampleContent,
                        PictureFormat = "image/png"
                    },

                    new Item
                    {
                        Id = Guid.Parse("d3e6f74f-84a5-4e05-ae4b-23bd94c620ff"),
                        UserId = "6e445865-a24d-4543-a6c6-9443d048cdb9",
                        Name = "Whimsical Garden Gnomes",
                        Description = "Add a touch of magic to your garden with our hand-painted gnomes! These charming figures are perfect for any green space, and their cheerful faces are sure to bring a smile to yours. A garden delight, waiting to find a home.",
                        Price = 12.91M,
                        Quantity = 1,
                        Picture = sampleContent,
                        PictureFormat = "image/png"
                    },

                    //USER KYLE SEEDED ITEMS
                    new Item
                    {
                        Id = Guid.Parse("d3e6f74f-74a5-4e05-ae4b-23bd94c620ff"),
                        UserId = "7e445865-a24d-4543-a6c6-9443d048cdb9",
                        Name = "Tech-Forward Wireless Earbuds",
                        Description = "Unleash the future of sound! Our cutting-edge earbuds offer crystal clear audio, paired with a sleek design and unmatched comfort. Sync, play, and dance to the rhythm of your life. Grab them now and feel the beat!",
                        Price = 18.78M,
                        Quantity = 19,
                        Picture = sampleContent,
                        PictureFormat = "image/png"
                    },

                    new Item
                    {
                        Id = Guid.Parse("d3e6f74f-64a5-4e05-ae4b-23bd94c620ff"),
                        UserId = "7e445865-a24d-4543-a6c6-9443d048cdb9",
                        Name = "Luxurious Cashmere Scarves",
                        Description = "Wrap yourself in sheer luxury. Our premium cashmere scarves are not only soft to the touch but also timeless in style. Perfect for chilly evenings or elevating any outfit. Elegance has never felt so cozy!",
                        Price = 1.3M,
                        Quantity = 8,
                        Picture = sampleContent,
                        PictureFormat = "image/png"
                    },

                    new Item
                    {
                        Id = Guid.Parse("d3e6f74f-54a5-4e05-ae4b-23bd94c620ff"),
                        UserId = "7e445865-a24d-4543-a6c6-9443d048cdb9",
                        Name = "Artisanal Coffee Beans",
                        Description = "Kickstart your day with our ethically sourced, hand-roasted coffee beans. Journey to a world of rich flavors and aromatic brews. Every cup is a story waiting to be told. Don't just wake up, come alive!",
                        Price = 1003.87M,
                        Quantity = 58,
                        Picture = sampleContent,
                        PictureFormat = "image/png"
                    },

                    new Item
                    {
                        Id = Guid.Parse("d3e6f74f-44a5-4e05-ae4b-23bd94c620ff"),
                        UserId = "7e445865-a24d-4543-a6c6-9443d048cdb9",
                        Name = "Mystical Crystal Pendants",
                        Description = "Unlock the secrets of the universe with our handcrafted crystal pendants. Worn close to the heart, each stone carries a unique energy. Fashion meets metaphysics; wear your vibe!",
                        Price = 61.23M,
                        Quantity = 102,
                        Picture = sampleContent,
                        PictureFormat = "image/png"
                    },

                    new Item
                    {
                        Id = Guid.Parse("d3e6f74f-34a5-4e05-ae4b-23bd94c620ff"),
                        UserId = "7e445865-a24d-4543-a6c6-9443d048cdb9",
                        Name = "Ergonomic Office Chair",
                        Description = "Reimagine comfort with our state-of-the-art office chairs. Designed for long hours and flawless posture, this is where productivity meets relaxation. Say goodbye to backaches and hello to efficiency!",
                        Price = 59.61M,
                        Quantity = 6,
                        Picture = sampleContent,
                        PictureFormat = "image/png"
                    },

                    //USER JOHN SEEDED ITEMS
                    new Item
                    {
                        Id = Guid.Parse("d3e6f74f-34a5-4e05-ae4b-33bd94c620ff"),
                        UserId = "5e445865-a24d-4543-a6c6-9443d048cdb9",
                        Name = "Upcycled Vintage Denim Jackets",
                        Description = "Embrace retro chic with our customized denim jackets. Hand-painted and reimagined, each piece tells a unique tale. Be sustainable, be stylish, and stand out!",
                        Price = 68.34M,
                        Quantity = 2,
                        Picture = sampleContent,
                        PictureFormat = "image/png"
                    },

                    new Item
                    {
                        Id = Guid.Parse("d3e6f74f-34a5-4e05-ae4b-43bd94c620ff"),
                        UserId = "5e445865-a24d-4543-a6c6-9443d048cdb9",
                        Name = "Organic Pet Treats",
                        Description = "Pamper your furry friends with our wholesome, organic treats. Made with love and the finest ingredients, these are sure to get tails wagging. Because they deserve the very best!",
                        Price = 1000000.00M,
                        Quantity = 100,
                        Picture = sampleContent,
                        PictureFormat = "image/png"
                    },

                    new Item
                    {
                        Id = Guid.Parse("d3e6f74f-34a5-4e05-ae4b-53bd94c620ff"),
                        UserId = "5e445865-a24d-4543-a6c6-9443d048cdb9",
                        Name = "Hand-Blown Glass Vases",
                        Description = "Elevate your living space with our exquisite glass vases. Each piece, a testament to craftsmanship, captures light and imagination in dazzling ways. Where art meets function.",
                        Price = 87.13M,
                        Quantity = 0,
                        Picture = sampleContent,
                        PictureFormat = "image/png"
                    },

                    new Item
                    {
                        Id = Guid.Parse("d3e6f74f-34a5-4e05-ae4b-63bd94c620ff"),
                        UserId = "5e445865-a24d-4543-a6c6-9443d048cdb9",
                        Name = "Traveler's Leather Journal",
                        Description = "Chronicle your journeys with our authentic leather journals. Perfect for the nomadic soul, its rustic pages are eager to be filled with tales of wanderlust. Every adventure deserves to be remembered.",
                        Price = 14.69M,
                        Quantity = 9,
                        Picture = sampleContent,
                        PictureFormat = "image/png"
                    },

                    new Item
                    {
                        Id = Guid.Parse("d3e6f74f-34a5-4e05-ae4b-73bd94c620ff"),
                        UserId = "5e445865-a24d-4543-a6c6-9443d048cdb9",
                        Name = "Sustainable Yoga Mats",
                        Description = "Align your mind, body, and ethos. Our eco-friendly yoga mats not only provide the perfect grip but also ensure a harmonious practice. Serenity now meets sustainability. Namaste!",
                        Price = 91.19M,
                        Quantity = 11,
                        Picture = sampleContent,
                        PictureFormat = "image/png"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
