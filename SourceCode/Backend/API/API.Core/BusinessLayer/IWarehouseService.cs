using System.Threading.Tasks;
using API.Core.EntityLayer.Warehouse;

namespace API.Core.BusinessLayer
{
    public interface IWarehouseService : IService
    {
        Task UpdatePriceProductAsync(Product entity, string client);
    }
}
