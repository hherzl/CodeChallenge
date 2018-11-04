using API.Core.BusinessLayer;
using API.Core.DataLayer;

namespace API.UnitTests.Mockers
{
    public class ServiceMocker
    {
        public static IWarehouseService GetWarehouseService(CodeChallengeDbContext dbContext)
            => new WarehouseService(dbContext);

        public static ISalesService GetSalesService(CodeChallengeDbContext dbContext)
            => new SalesService(dbContext);
    }
}
