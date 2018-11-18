using System.Threading.Tasks;
using API.Core.EntityLayer.Warehouse;

namespace API.Core.BusinessLayer
{
    public interface IWarehouseService : IService
    {
        Task<int> CreateProductAsync(Product entity);

        Task<int> UpdatePriceProductAsync(Product entity);

        Task<int> LikeProductAsync(Product entity);

        Task<int> DeleteProductAsync(Product entity);
    }
}
