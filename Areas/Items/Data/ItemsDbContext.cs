using Microsoft.EntityFrameworkCore;
using SolutionSelling.Models;

namespace SolutionSelling.Areas.Items.Data
{
    public class ItemsDbContext : DbContext
    {
        public ItemsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Item> Item { get; set; }

        public DbSet<Purchases> Purchases { get; set; }

    }
}
