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

            WarehouseRepository.Add(entity);

            await CommitChangesAsync();
        }

        public async Task UpdatePriceProductAsync(Product entity)
        {
            using (var txn = await DbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Set changes
                    entity.Price = entity.Price;

                    WarehouseRepository.Update(entity);

                    await CommitChangesAsync();

                    // Add product price to history
                    var history = new ProductPriceHistory
                    {
                        ProductID = entity.ProductID,
                        Price = entity.Price,
                        StartDate = DateTime.Now,
                        CreationUser = entity.LastUpdateUser
                    };

                    WarehouseRepository.Add(history);

                    await CommitChangesAsync();

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

            WarehouseRepository.Update(entity);

            await CommitChangesAsync();
        }
    }
}
