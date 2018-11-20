using System;
using System.Threading.Tasks;
using API.Core.DataLayer;
using API.Core.EntityLayer.Warehouse;
using Microsoft.EntityFrameworkCore;

namespace API.Core.BusinessLayer
{
    public class WarehouseService : Service, IWarehouseService
    {
        public WarehouseService(StoreDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<int> CreateProductAsync(Product entity)
        {
            // Set default values
            entity.Likes = 0;
            entity.Stocks = 0;
            entity.Available = true;

            DbContext.AddEntity(entity);

            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> UpdatePriceProductAsync(Product entity)
        {
            using (var txn = await DbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Set changes
                    entity.Price = entity.Price;

                    DbContext.UpdateEntity(entity);

                    await DbContext.SaveChangesAsync();

                    // Add product price to history
                    DbContext.AddEntity(new ProductPriceHistory
                    {
                        ProductID = entity.ProductID,
                        Price = entity.Price,
                        StartDate = DateTime.Now,
                        CreationUser = entity.LastUpdateUser
                    });

                    var affectedRows = await DbContext.SaveChangesAsync();

                    txn.Commit();

                    return affectedRows;
                }
                catch (Exception ex)
                {
                    txn.Rollback();

                    throw ex;
                }
            }
        }

        public async Task<int> LikeProductAsync(Product entity)
        {
            var productLike = await DbContext
                .GetProductLikeByProductIDAndCreationUserAsync(entity.ProductID, entity.LastUpdateUser);

            if (productLike == null)
            {
                DbContext.AddEntity(new ProductLike
                {
                    ProductID = entity.ProductID,
                    CreationUser = entity.LastUpdateUser,
                    CreationDateTime = DateTime.Now
                });

                entity.Likes += 1;

                DbContext.UpdateEntity(entity);

                return await DbContext.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> DeleteProductAsync(Product entity)
        {
            var count = await DbContext
                .GetOrderDetails(productID: entity.ProductID).CountAsync();

            if (count > 0)
                throw new ApiException(string.Format("The product: '{0}', with ID: '{1}' cannot be deleted, has dependencies in sales.", entity.ProductName, entity.ProductID));

            DbContext.Remove(entity);

            return await DbContext.SaveChangesAsync();
        }
    }
}
