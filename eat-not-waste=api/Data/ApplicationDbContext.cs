using eat_not_waste_api.Models;
using eat_not_waste_api.DTOs;
using Microsoft.EntityFrameworkCore;
using System;

namespace eat_not_waste_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        // name of tables in db
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                Id = 1,
                FirstName = "John",
                Surname = "Sample",
                Email = "sample@mail.com",
                Password = "pass1234",
                Location = "Sample City",
            });

            modelBuilder.Entity<Merchant>().HasData(new Merchant
            {
                Id = 1,
                Name = "Sushi City",
                Email = "hello@sushicity.com",
                Password = "pass1234",
                Location = "Manchester",
                Category = "Sushi",
                Description = "Tasty sushi made fresh"
            });

            modelBuilder.Entity<Listing>().HasData(new Listing
            {
                Id = 1,
                MerchantId = 1,
                Title = "Veggie bento box",
                Description = "£12 worth for £3!",
                Location = "Manchester",
                Quantity = 5,
                Price = 12,
                ExpirationDate = DateTime.Now.AddDays(30)
            });

            modelBuilder.Entity<Purchase>().HasData(new Purchase
            {
                Id = 1,
                CustomerId = 1,
                ListingId = 1,
                Quantity = 1,
                PurchaseDate = DateTime.Now
            });

            modelBuilder.Entity<Review>().HasData(new Review
            {
                Id = 1,
                CustomerId = 1,
                MerchantId = 1,
                Rating = 5,
                Comment = "Great Service!"
            });

            modelBuilder.Entity<Listing>()
                .HasOne<Merchant>(listing => listing.Merchant)
                .WithMany(merchant => merchant.Listings)
                .HasForeignKey(l => l.MerchantId);

            modelBuilder.Entity<Purchase>()
                .HasOne<Customer>(purchase => purchase.Customer)
                .WithMany(customer => customer.Purchases)
                .HasForeignKey(purchase => purchase.CustomerId);

            modelBuilder.Entity<Purchase>()
                .HasOne<Listing>(purchase => purchase.Listing)
                .WithMany(listing => listing.Purchases)
                .HasForeignKey(purchase => purchase.ListingId);

            modelBuilder.Entity<Review>()
                .HasOne<Customer>(review => review.Customer)
                .WithMany(customer => customer.Reviews)
                .HasForeignKey(review => review.CustomerId);

            modelBuilder.Entity<Review>()
                .HasOne<Merchant>(review => review.Merchant)
                .WithMany(merchant => merchant.Reviews)
                .HasForeignKey(r => r.MerchantId);
        }
    }
}