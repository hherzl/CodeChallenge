using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Core.EntityLayer.Sales;
using API.Core.DataLayer.Contracts;
using API.Core.EntityLayer.Warehouse;

namespace API.Core.DataLayer.Contracts
{
	public interface ISalesRepository : IRepository
	{
		IQueryable<OrderDetail> GetOrderDetails(Int32? orderHeaderID = null, Int32? productID = null);

		Task<OrderDetail> GetOrderDetailAsync(OrderDetail entity);

		IQueryable<OrderHeader> GetOrderHeaders();

		Task<OrderHeader> GetOrderHeaderAsync(OrderHeader entity);
	}
}
