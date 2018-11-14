using API.Core.DataLayer;

namespace API.Core.BusinessLayer
{
    public abstract class Service
    {
        private bool Disposed;

        public Service(StoreDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Dispose()
        {
            if (!Disposed)
            {
                DbContext?.Dispose();

                Disposed = true;
            }
        }

        public StoreDbContext DbContext { get; }
    }
}
