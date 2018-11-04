using API.Core.DataLayer;

namespace API.Core.BusinessLayer
{
    public class SalesService : Service, ISalesService
    {
        public SalesService(CodeChallengeDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
