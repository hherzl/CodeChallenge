using API.Core.BusinessLayer;
using API.Core.DataLayer;

namespace API.UnitTests.Mockers
{
    public class ServiceMocker
    {
        public static IWarehouseService GetWarehouseService(StoreDbContext dbContext)
            => new WarehouseService(dbContext);

        public static ISalesService GetSalesService(StoreDbContext dbContext)
            => new SalesService(dbContext);
    }
}
