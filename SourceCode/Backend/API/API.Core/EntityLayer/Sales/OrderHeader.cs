using System;
using System.Collections.ObjectModel;
using API.Core.EntityLayer.Sales;

namespace API.Core.EntityLayer.Sales
{
	public partial class OrderHeader : IAuditEntity
	{
		public OrderHeader()
		{
		}

		public OrderHeader(Int32? orderHeaderID)
		{
			OrderHeaderID = orderHeaderID;
		}

		public Int32? OrderHeaderID { get; set; }

		public DateTime? OrderDate { get; set; }

		public Decimal? Total { get; set; }

		public String CreationUser { get; set; }

		public DateTime? CreationDateTime { get; set; }

		public String LastUpdateUser { get; set; }

		public DateTime? LastUpdateDateTime { get; set; }

		public Byte[] Timestamp { get; set; }

		public Collection<OrderDetail> OrderDetails { get; set; }
	}
}
