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
                    var history = new ProductPriceHistory
                    {
                        ProductID = entity.ProductID,
                        Price = entity.Price,
                        StartDate = DateTime.Now,
                        CreationUser = entity.LastUpdateUser
                    };

                    DbContext.Add(history);

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

        public async Task LikeProductAsync(Product entity)
        {
            // Set changes
            entity.Likes += 1;

            // todo: Save who likes the product in history

            DbContext.Update(entity);

            await DbContext.SaveChangesAsync();
        }
    }
}
