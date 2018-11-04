using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.EntityLayer.Sales;

namespace API.Core.BusinessLayer
{
    public interface ISalesService : IService
    {
        Task PlaceOrderAsync(OrderHeader header, IEnumerable<OrderDetail> details);
    }
}
