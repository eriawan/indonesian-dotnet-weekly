using Microsoft.EntityFrameworkCore;

namespace SampleAspNetAspire90.ApiService.DataContext
{
    public class AspireDbContext : DbContext
    {
        public AspireDbContext(DbContextOptions<AspireDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Product { get; set; } = default!;
    }

    public static class DbExtensions
    {
        public static void CreateDbIfNotExists(this IHost host)
        {
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AspireDbContext>();
            context.Database.EnsureCreated();
            DbInitializer.Initialize(context);
        }
    }

    public static class DbInitializer
    {
        public static void Initialize(AspireDbContext context)
        {
            if (context.Product.Any())
                return;

            var products = new List<Product>
            {
                new() { ProductName = "Solar Powered Flashlight", ProductDescription = "A fantastic product for outdoor enthusiasts", ProductPrice = 19.99m, ProductColor = "Blue" },
                new() { ProductName = "Hiking Poles", ProductDescription = "Ideal for camping and hiking trips", ProductPrice = 24.99m, ProductColor = "White" },
                new() { ProductName = "Outdoor Rain Jacket", ProductDescription = "This product will keep you warm and dry in all weathers", ProductPrice = 49.99m, ProductColor = "Blue" },
                new() { ProductName = "Survival Kit", ProductDescription = "A must-have for any outdoor adventurer", ProductPrice = 99.99m, ProductColor = "Blue" },
                new() { ProductName = "Outdoor Backpack", ProductDescription = "This backpack is perfect for carrying all your outdoor essentials", ProductPrice = 39.99m, ProductColor = "Red" },
                new() { ProductName = "Camping Cookware", ProductDescription = "This cookware set is ideal for cooking outdoors", ProductPrice = 29.99m, ProductColor = "Red" },
                new() { ProductName = "Camping Stove", ProductDescription = "This stove is perfect for cooking outdoors", ProductPrice = 49.99m, ProductColor = "product7.png" }
            };

            context.AddRange(products);

            context.SaveChanges();
        }
    }
}
