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

        public async Task UpdatePriceProductAsync(Product entity, string client)
        {
            using (var txn = await DbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Set changes
                    entity.Price = entity.Price;
                    entity.LastUpdateUser = client;

                    // Update entity to database
                    WarehouseRepository.Update(entity);

                    await CommitChangesAsync();

                    // Add product price to history
                    var history = new ProductPriceHistory
                    {
                        ProductID = entity.ProductID,
                        Price = entity.Price,
                        StartDate = DateTime.Now,
                        CreationUser = client
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

        public async Task LikeProductAsync(Product entity, string client)
        {
            // Set changes
            entity.Likes += 1;
            entity.LastUpdateUser = client;

            // Update entity to database
            WarehouseRepository.Update(entity);

            await CommitChangesAsync();
        }
    }
}
