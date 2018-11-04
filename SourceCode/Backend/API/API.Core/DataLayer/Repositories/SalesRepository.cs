using System.Linq;
using System.Threading.Tasks;
using API.Core.DataLayer.Contracts;
using API.Core.EntityLayer.Sales;
using Microsoft.EntityFrameworkCore;

namespace API.Core.DataLayer.Repositories
{
    public class SalesRepository : Repository, ISalesRepository
    {
        public SalesRepository(StoreDbContext dbContext)
            : base(dbContext)
        {
        }

        public IQueryable<OrderDetail> GetOrderDetails(int? orderHeaderID = null, int? productID = null)
        {
            // Get query from DbSet
            var query = DbContext.Set<OrderDetail>().AsQueryable();

            // Filter by: 'OrderHeaderID'
            if (orderHeaderID.HasValue)
                query = query.Where(item => item.OrderHeaderID == orderHeaderID);

            // Filter by: 'ProductID'
            if (productID.HasValue)
                query = query.Where(item => item.ProductID == productID);

            return query;
        }

        public async Task<OrderDetail> GetOrderDetailAsync(OrderDetail entity)
            => await DbContext.Set<OrderDetail>().FirstOrDefaultAsync(item => item.OrderDetailID == entity.OrderDetailID);

        public IQueryable<OrderHeader> GetOrderHeaders()
        {
            // Get query from DbSet
            var query = DbContext.Set<OrderHeader>().AsQueryable();

            return query;
        }

        public async Task<OrderHeader> GetOrderHeaderAsync(OrderHeader entity)
            => await DbContext.Set<OrderHeader>().FirstOrDefaultAsync(item => item.OrderHeaderID == entity.OrderHeaderID);
    }
}
