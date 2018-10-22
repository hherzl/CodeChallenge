using System;
using API.Core.EntityLayer.Warehouse;

namespace API.Core.EntityLayer.Sales
{
    public partial class OrderDetail : IAuditEntity
	{
		public OrderDetail()
		{
		}

		public OrderDetail(int? orderDetailID)
		{
			OrderDetailID = orderDetailID;
		}

		public int? OrderDetailID { get; set; }

		public int? OrderHeaderID { get; set; }

		public int? ProductID { get; set; }

		public string ProductName { get; set; }

		public decimal? UnitPrice { get; set; }

		public int? Quantity { get; set; }

		public decimal? Total { get; set; }

		public string CreationUser { get; set; }

		public DateTime? CreationDateTime { get; set; }

		public string LastUpdateUser { get; set; }

		public DateTime? LastUpdateDateTime { get; set; }

		public byte[] Timestamp { get; set; }

		public OrderHeader OrderHeaderFk { get; set; }

		public Product ProductFk { get; set; }
	}
}
