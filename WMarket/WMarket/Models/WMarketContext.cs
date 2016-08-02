using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WMarket.Models
{
    public class WMarketContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public WMarketContext() : base("name=WMarketContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<WMarket.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<WMarket.Models.DocumentType> DocumentTypes { get; set; }

        public System.Data.Entity.DbSet<WMarket.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<WMarket.Models.Supplier> Suppliers { get; set; }

        public System.Data.Entity.DbSet<WMarket.Models.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<WMarket.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<WMarket.Models.OrderDetail> OrderDetails { get; set; }

        public System.Data.Entity.DbSet<WMarket.Models.Inventories> Inventories { get; set; }

        public System.Data.Entity.DbSet<WMarket.Models.InventoryDetail> InventoryDetails { get; set; }

        public System.Data.Entity.DbSet<WMarket.Models.Shopping> Shopping { get; set; }

        public System.Data.Entity.DbSet<WMarket.Models.ShoppingDetail> ShoppingDetails { get; set; }

        public System.Data.Entity.DbSet<WMarket.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<WMarket.Models.Tax> Taxes { get; set; }

        public System.Data.Entity.DbSet<WMarket.Models.Sale> Sales { get; set; }

        public System.Data.Entity.DbSet<WMarket.Models.SaleDetail> SaleDetails { get; set; }
    
    }
}
