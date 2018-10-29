using System;
using API.Core.DataLayer;
using API.Core.EntityLayer.Warehouse;

namespace API.UnitTests
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

            dbContext.SaveChanges();
        }
    }
}
