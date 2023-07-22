using eat_not_waste_api.Models;
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
        // name of table in db
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
            new Villa()
            {
                Id = 1,
                Name = "Royal Villa",
                Details = "Fusce pfeso fsfeojosef  jfeosjfe jsoefjsefj seofjsoeijf oj",
                ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fen.wikipedia.org%2Fwiki%2FImage&psig=AOvVaw2Xbv-RKWdm38bG-2HloM87&ust=1687816504753000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCIDwjMy03_8CFQAAAAAdAAAAABAE",
                Occupancy = 5,
                Rate = 200,
                Sqft = 550,
                Amenity = "",
                CreatedDate = DateTime.Now,
            },
            new Villa()
            {
                Id = 2,
                Name = "Seafront Villa",
                Details = "This is a beautiful villa located right at the seafront, providing a mesmerizing view of the ocean.",
                ImageUrl = "https://example.com/seafront-villa.jpg",
                Occupancy = 6,
                Rate = 350,
                Sqft = 800,
                Amenity = "Private beach, swimming pool, garden",
                CreatedDate = DateTime.Now,

            },
            new Villa()
            {
                Id = 3,
                Name = "Mountain Villa",
                Details = "A luxurious villa located in the mountains, perfect for those seeking solitude and a beautiful view.",
                ImageUrl = "https://example.com/mountain-villa.jpg",
                Occupancy = 4,
                Rate = 300,
                Sqft = 700,
                Amenity = "Hot tub, fireplace, ski-in/ski-out",
                CreatedDate = DateTime.Now,
            },
            new Villa()
            {
                Id = 4,
                Name = "Urban Villa",
                Details = "Located in the heart of the city, this villa offers a unique combination of luxury and accessibility.",
                ImageUrl = "https://example.com/urban-villa.jpg",
                Occupancy = 5,
                Rate = 400,
                Sqft = 900,
                Amenity = "Rooftop terrace, home theater, home gym",
                CreatedDate = DateTime.Now,
            }
          );
        }
    }
}