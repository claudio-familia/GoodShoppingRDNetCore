using GoodShoppingRD.Models.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodShoppingRD.Models
{
    public class GoodShoppingRdDbContext: IdentityDbContext<User, Role, Guid>
    {
        public GoodShoppingRdDbContext(DbContextOptions options):base(options)
        {
        }

        public DbSet<Supermarket> Supermarkets { get; set; }
        public DbSet<SupermarketAddress> SupermarketAddresses { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCatalog> ProductCatalogs { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartDetail> ShoppingCartDetails { get; set; }
    }
}
