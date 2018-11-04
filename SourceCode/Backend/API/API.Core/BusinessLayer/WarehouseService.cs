using API.Core.DataLayer;

namespace API.Core.BusinessLayer
{
    public class WarehouseService : Service, IWarehouseService
    {
        public WarehouseService(StoreDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
