using API.Core.DataLayer;

namespace API.Core.BusinessLayer
{
    public class WarehouseService : Service, IWarehouseService
    {
        public WarehouseService(CodeChallengeDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
