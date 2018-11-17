using System;

namespace API.Core.EntityLayer.Warehouse
{
    public class ProductLike : IAuditEntity
    {
        public ProductLike()
        {
        }

        public ProductLike(int? productLikeID)
        {
            ProductLikeID = productLikeID;
        }

        public int? ProductLikeID { get; set; }

        public int? ProductID { get; set; }

        public string CreationUser { get; set; }

        public DateTime? CreationDateTime { get; set; }

        public string LastUpdateUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public byte[] Timestamp { get; set; }
    }
}
