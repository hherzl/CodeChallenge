using System;
using System.Threading.Tasks;
using API.Core.DataLayer;
using API.Core.DataLayer.Contracts;

namespace API.Core.BusinessLayer
{
    public interface IService : IDisposable
    {
        StoreDbContext DbContext { get; }

        IWarehouseRepository WarehouseRepository { get; }

        ISalesRepository SalesRepository { get; }

        int CommitChanges();

        Task<int> CommitChangesAsync();
    }
}
