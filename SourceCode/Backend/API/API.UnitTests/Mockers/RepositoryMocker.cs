using API.Core.DataLayer;
using API.Core.DataLayer.Contracts;
using API.Core.DataLayer.Repositories;

namespace API.UnitTests.Mockers
{
    public static class RepositoryMocker
    {
        public static IWarehouseRepository GetWarehouseRepository(CodeChallengeDbContext dbContext)
            => new WarehouseRepository(dbContext);

        public static ISalesRepository GetSalesRepository(CodeChallengeDbContext dbContext)
            => new SalesRepository(dbContext);
    }
}
