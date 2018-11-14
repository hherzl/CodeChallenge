using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core.DataLayer;
using API.Core.EntityLayer.Sales;
using API.Core.EntityLayer.Warehouse;

namespace API.Core.BusinessLayer
{
    public class SalesService : Service, ISalesService
    {
        public SalesService(StoreDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task PlaceOrderAsync(OrderHeader header, IEnumerable<OrderDetail> details)
        {
            using (var txn = await DbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Set default values for order header
                    header.OrderDate = DateTime.Now;
                    header.Total = 0m;
                    header.Total = details.Sum(item => item.Total);

                    DbContext.Add(header);

                    await DbContext.SaveChangesAsync();

                    foreach (var item in details)
                    {
                        item.OrderHeaderID = header.OrderHeaderID;

                        DbContext.Add(item);
                    }

                    await DbContext.SaveChangesAsync();

                    foreach (var detail in details)
                    {
                        var product = await DbContext.GetProductAsync(new Product(detail.ProductID));

                        // Update stocks for product
                        product.Stocks -= 1;
                    }

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
    }
}
