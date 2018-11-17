using System;
using System.Threading.Tasks;
using API.Core.DataLayer;
using API.Core.EntityLayer.Warehouse;

namespace API.Core.BusinessLayer
{
    public class WarehouseService : Service, IWarehouseService
    {
        public WarehouseService(StoreDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task CreateProductAsync(Product entity)
        {
            // Set default values
            entity.Likes = 0;
            entity.Stocks = 0;
            entity.Available = true;

            DbContext.Add(entity);

            await DbContext.SaveChangesAsync();
        }

        public async Task UpdatePriceProductAsync(Product entity)
        {
            using (var txn = await DbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Set changes
                    entity.Price = entity.Price;

                    DbContext.Update(entity);

                    await DbContext.SaveChangesAsync();

                    // Add product price to history
                    DbContext.Add(new ProductPriceHistory
                    {
                        ProductID = entity.ProductID,
                        Price = entity.Price,
                        StartDate = DateTime.Now,
                        CreationUser = entity.LastUpdateUser
                    });

                    await DbContext.SaveChangesAsync();

                    txn.Commit();
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
            var productLike = await DbContext.GetProductLikeByProductIDAndCreationUserAsync(entity.ProductID, entity.LastUpdateUser);

            if (productLike == null)
            {
                DbContext.Add(new ProductLike { ProductID = entity.ProductID, CreationUser = entity.LastUpdateUser, CreationDateTime = DateTime.Now });

                entity.Likes += 1;

                DbContext.Update(entity);

                return await DbContext.SaveChangesAsync();
            }

            return 0;
        }
    }
}
