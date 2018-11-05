using System.Threading.Tasks;
using API.Core.EntityLayer.Warehouse;

namespace API.Core.BusinessLayer
{
    public interface IWarehouseService : IService
    {
        Task CreateProductAsync(Product entity);

        Task UpdatePriceProductAsync(Product entity);

        Task LikeProductAsync(Product entity);
    }
}
