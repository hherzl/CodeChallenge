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

		public Product(Int32? productID)
		{
			ProductID = productID;
		}

		public Int32? ProductID { get; set; }

		public String ProductName { get; set; }

		public String ProductDescription { get; set; }

		public Decimal? Price { get; set; }

		public Int32? Likes { get; set; }

		public Int32? Stocks { get; set; }

		public Boolean? Available { get; set; }

		public String CreationUser { get; set; }

		public DateTime? CreationDateTime { get; set; }

		public String LastUpdateUser { get; set; }

		public DateTime? LastUpdateDateTime { get; set; }

		public Byte[] Timestamp { get; set; }

		public Collection<OrderDetail> OrderDetails { get; set; }
	}
}
