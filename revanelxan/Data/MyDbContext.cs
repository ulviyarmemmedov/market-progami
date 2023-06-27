using Microsoft.EntityFrameworkCore;
using revanelxan.Models;

namespace revanelxan.Data
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
