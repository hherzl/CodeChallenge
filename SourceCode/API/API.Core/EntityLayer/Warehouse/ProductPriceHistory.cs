using System;

namespace API.Core.EntityLayer.Warehouse
{
	public partial class ProductPriceHistory : IAuditEntity
	{
		public ProductPriceHistory()
		{
		}

		public ProductPriceHistory(Int32? productID)
		{
			ProductID = productID;
		}

		public Int32? ProductPriceHistoryID { get; set; }

		public Int32? ProductID { get; set; }

		public Decimal? Price { get; set; }

		public DateTime? StartDate { get; set; }

		public String CreationUser { get; set; }

		public DateTime? CreationDateTime { get; set; }

		public String LastUpdateUser { get; set; }

		public DateTime? LastUpdateDateTime { get; set; }

		public Byte[] Timestamp { get; set; }
	}
}
