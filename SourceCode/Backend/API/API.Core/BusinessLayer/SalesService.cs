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
                    header.Total = details.Sum(item => item.Total);

                    SalesRepository.Add(header);

                    await CommitChangesAsync();

                    foreach (var item in details)
                    {
                        item.OrderHeaderID = header.OrderHeaderID;

                        SalesRepository.Add(item);
                    }

                    await CommitChangesAsync();

                    foreach (var detail in details)
                    {
                        var product = await WarehouseRepository.GetProductAsync(new Product(detail.ProductID));

                        product.Stocks -= 1;
                    }

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
    }
}
