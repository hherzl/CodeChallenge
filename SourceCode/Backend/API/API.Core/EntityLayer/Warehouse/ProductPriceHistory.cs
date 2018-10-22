using System;

namespace API.Core.EntityLayer.Warehouse
{
	public partial class ProductPriceHistory : IAuditEntity
	{
		public ProductPriceHistory()
		{
		}

		public ProductPriceHistory(int? productID)
		{
			ProductID = productID;
		}

		public int? ProductPriceHistoryID { get; set; }

		public int? ProductID { get; set; }

		public decimal? Price { get; set; }

		public DateTime? StartDate { get; set; }

		public string CreationUser { get; set; }

		public DateTime? CreationDateTime { get; set; }

		public string LastUpdateUser { get; set; }

		public DateTime? LastUpdateDateTime { get; set; }

		public byte[] Timestamp { get; set; }
	}
}
