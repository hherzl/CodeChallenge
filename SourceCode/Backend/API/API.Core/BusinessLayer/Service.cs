using System.Threading.Tasks;
using API.Core.DataLayer;
using API.Core.DataLayer.Contracts;
using API.Core.DataLayer.Repositories;

namespace API.Core.BusinessLayer
{
    public abstract class Service
    {
        private bool Disposed;
        private IWarehouseRepository m_warehouseRepository;
        private ISalesRepository m_salesRepository;

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

        public IWarehouseRepository WarehouseRepository
        {
            get => m_warehouseRepository = new WarehouseRepository(DbContext);
        }

        public ISalesRepository SalesRepository
        {
            get => m_salesRepository = new SalesRepository(DbContext);
        }

        public int CommitChanges()
            => DbContext.SaveChanges();

        public Task<int> CommitChangesAsync()
            => DbContext.SaveChangesAsync();
    }
}
