using System;
using API.Core.DataLayer;

namespace API.Core.BusinessLayer
{
    public interface IService : IDisposable
    {
        StoreDbContext DbContext { get; }
    }
}
