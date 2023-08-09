﻿using e_trade_api.domain;
using e_trade_api.domain.Entities;
using e_trade_api.domain.Entities.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace e_trade_api.Persistence.Contexts
{
    public class ETradeApiDBContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ETradeApiDBContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<domain.File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        // protected override void OnModelCreating(ModelBuilder modelBuilder) //migrationda bir şeyi değiştirmedi
        // {
        //     modelBuilder
        //         .Entity<Order>()
        //         .HasMany(o => o.OrderItems)
        //         .WithOne(oi => oi.Order)
        //         .HasForeignKey(oi => oi.OrderId);

        //     modelBuilder
        //         .Entity<AppUser>()
        //         .HasMany(u => u.Orders)
        //         .WithOne(o => o.User)
        //         .HasForeignKey(o => o.UserId);

        //     modelBuilder
        //         .Entity<Product>()
        //         .HasMany(p => p.OrderItems)
        //         .WithOne(p => p.Product)
        //         .HasForeignKey(p => p.ProductId);

        //     base.OnModelCreating(modelBuilder);
        // }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default
        )
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                if (data.State == EntityState.Added)
                {
                    data.Entity.CreatedDate = DateTime.Now;
                }
                if (data.State == EntityState.Modified)
                {
                    data.Entity.UpdatedDate = DateTime.Now;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
