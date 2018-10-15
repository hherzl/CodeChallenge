using System;
using API.Core.EntityLayer.Sales;
using API.Core.EntityLayer.Warehouse;

namespace API.Core.EntityLayer.Sales
{
	public partial class OrderDetail : IAuditEntity
	{
		public OrderDetail()
		{
		}

		public OrderDetail(Int32? orderDetailID)
		{
			OrderDetailID = orderDetailID;
		}

		public Int32? OrderDetailID { get; set; }

		public Int32? OrderHeaderID { get; set; }

		public Int32? ProductID { get; set; }

		public String ProductName { get; set; }

		public Decimal? UnitPrice { get; set; }

		public Int32? Quantity { get; set; }

		public Decimal? Total { get; set; }

		public String CreationUser { get; set; }

		public DateTime? CreationDateTime { get; set; }

		public String LastUpdateUser { get; set; }

		public DateTime? LastUpdateDateTime { get; set; }

		public Byte[] Timestamp { get; set; }

		public OrderHeader OrderHeaderFk { get; set; }

		public Product ProductFk { get; set; }
	}
}
