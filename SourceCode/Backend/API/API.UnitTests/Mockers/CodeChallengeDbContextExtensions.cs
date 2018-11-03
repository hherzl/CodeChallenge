using System;
using API.Core.DataLayer;
using API.Core.EntityLayer.Warehouse;

namespace API.UnitTests.Mockers
{
    public static class CodeChallengeDbContextExtensions
    {
        public static void SeedInMemory(this CodeChallengeDbContext dbContext)
        {
            dbContext.Set<Product>().Add(new Product
            {
                ProductID = 1,
                ProductName = "Coca Cola 24 fl Oz Bottle",
                ProductDescription = "Enjoy Coca-Cola’s crisp, delicious taste with meals, on the go, or to share. Serve ice cold for maximum refreshment.",
                Price = 1.99m,
                Likes = 0,
                Stocks = 100,
                Available = true,
                CreationUser = "seed",
                CreationDateTime = DateTime.Now
            });

            dbContext.Set<Product>().Add(new Product
            {
                ProductID = 2,
                ProductName = "Diet Coca Cola 24 fl Oz Bottle",
                ProductDescription = "Enjoy Coca-Cola’s crisp, delicious taste with meals, on the go, or to share. Serve ice cold for maximum refreshment.",
                Price = 1.99m,
                Likes = 0,
                Stocks = 100,
                Available = true,
                CreationUser = "seed",
                CreationDateTime = DateTime.Now
            });

            dbContext.Set<Product>().Add(new Product
            {
                ProductID = 3,
                ProductName = "Coca Cola 8.5 Oz Aluminum Bottle",
                ProductDescription = "Enjoy Coca-Cola’s crisp, delicious taste with meals, on the go, or to share. Serve ice cold for maximum refreshment.",
                Price = 1.99m,
                Likes = 0,
                Stocks = 100,
                Available = true,
                CreationUser = "seed",
                CreationDateTime = DateTime.Now
            });

            dbContext.Set<Product>().Add(new Product
            {
                ProductID = 4,
                ProductName = "Diet Coca Cola 8.5 Oz Aluminum Bottle",
                ProductDescription = "Enjoy Coca-Cola’s crisp, delicious taste with meals, on the go, or to share. Serve ice cold for maximum refreshment.",
                Price = 1.99m,
                Likes = 0,
                Stocks = 100,
                Available = true,
                CreationUser = "seed",
                CreationDateTime = DateTime.Now
            });

            dbContext.Set<Product>().Add(new Product
            {
                ProductID = 5,
                ProductName = "Coca Cola Zero 24 fl Oz Bottle",
                ProductDescription = "Enjoy Coca-Cola’s crisp, delicious taste with meals, on the go, or to share. Serve ice cold for maximum refreshment.",
                Price = 1.99m,
                Likes = 0,
                Stocks = 100,
                Available = true,
                CreationUser = "seed",
                CreationDateTime = DateTime.Now
            });

            dbContext.Set<Product>().Add(new Product
            {
                ProductID = 6,
                ProductName = "Diet Coca Cola Zero 24 fl Oz Bottle",
                ProductDescription = "Enjoy Coca-Cola’s crisp, delicious taste with meals, on the go, or to share. Serve ice cold for maximum refreshment.",
                Price = 1.99m,
                Likes = 0,
                Stocks = 100,
                Available = true,
                CreationUser = "seed",
                CreationDateTime = DateTime.Now
            });

            dbContext.SaveChanges();
        }
    }
}
