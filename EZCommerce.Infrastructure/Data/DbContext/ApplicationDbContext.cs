using EZCommerce.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EZCommerce.Infrastructure.Data.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        private static bool _isModelCreatingExecuted = false;

        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Discount> discounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (!_isModelCreatingExecuted)
            {
                ApplyConfigurationsFromNamespace(modelBuilder, "EZCommerce.Infrastructure.Data.Configurations");
                _isModelCreatingExecuted = true;
            }
        }
        private void ApplyConfigurationsFromNamespace(ModelBuilder modelBuilder, string @namespace)
        {
            var configurationTypes = Assembly.GetExecutingAssembly()
                                             .GetTypes()
                                             .Where(type => type.Namespace != null &&
                                                            type.Namespace.StartsWith(@namespace) &&
                                                            !type.IsAbstract &&
                                                            type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            foreach (var configuration in configurationTypes)
            {
                dynamic? configurationInstance = Activator.CreateInstance(configuration);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}
