using System;
using System.Threading.Tasks;
using API.Core.DataLayer.Contracts;

namespace API.Core.BusinessLayer
{
    public interface IService : IDisposable
    {
        IWarehouseRepository WarehouseRepository { get; }

        ISalesRepository SalesRepository { get; }

        int CommitChanges();

        Task<int> CommitChangesAsync();
    }
}
