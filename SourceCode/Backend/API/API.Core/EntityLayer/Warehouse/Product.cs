using System;
using System.Collections.ObjectModel;
using API.Core.EntityLayer.Sales;

namespace API.Core.EntityLayer.Warehouse
{
    public partial class Product : IAuditEntity
	{
		public Product()
		{
		}

		public Product(int? productID)
		{
			ProductID = productID;
		}

		public int? ProductID { get; set; }

		public string ProductName { get; set; }

		public string ProductDescription { get; set; }

		public decimal? Price { get; set; }

		public int? Likes { get; set; }

		public int? Stocks { get; set; }

		public bool? Available { get; set; }

		public string CreationUser { get; set; }

		public DateTime? CreationDateTime { get; set; }

		public string LastUpdateUser { get; set; }

		public DateTime? LastUpdateDateTime { get; set; }

		public byte[] Timestamp { get; set; }

		public Collection<OrderDetail> OrderDetails { get; set; }
	}
}
