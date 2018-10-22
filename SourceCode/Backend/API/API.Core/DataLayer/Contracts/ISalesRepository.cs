using System.Linq;
using System.Threading.Tasks;
using API.Core.EntityLayer.Sales;

namespace API.Core.DataLayer.Contracts
{
    public interface ISalesRepository : IRepository
	{
		IQueryable<OrderDetail> GetOrderDetails(int? orderHeaderID = null, int? productID = null);

		Task<OrderDetail> GetOrderDetailAsync(OrderDetail entity);

		IQueryable<OrderHeader> GetOrderHeaders();

		Task<OrderHeader> GetOrderHeaderAsync(OrderHeader entity);
	}
}
