using System;
using System.Collections.ObjectModel;

namespace API.Core.EntityLayer.Sales
{
    public partial class OrderHeader : IAuditEntity
	{
		public OrderHeader()
		{
		}

		public OrderHeader(int? orderHeaderID)
		{
			OrderHeaderID = orderHeaderID;
		}

		public int? OrderHeaderID { get; set; }

		public DateTime? OrderDate { get; set; }

		public decimal? Total { get; set; }

		public string CreationUser { get; set; }

		public DateTime? CreationDateTime { get; set; }

		public string LastUpdateUser { get; set; }

		public DateTime? LastUpdateDateTime { get; set; }

		public byte[] Timestamp { get; set; }

		public Collection<OrderDetail> OrderDetails { get; set; }
	}
}
