using System;
using API.Core.DataLayer;
using API.Core.DataLayer.Contracts;

namespace API.Core.BusinessLayer
{
    public interface IService : IDisposable
    {
        CodeChallengeDbContext DbContext { get; }

        IWarehouseRepository WarehouseRepository { get; }

        ISalesRepository SalesRepository { get; }
    }
}
