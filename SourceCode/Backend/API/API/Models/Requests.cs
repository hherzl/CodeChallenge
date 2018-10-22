using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using API.Core.EntityLayer.Warehouse;

namespace API.Models
{
    public class AddProductRequestModel
    {
        [Key]
        public int? ProductID { get; set; }

        [Required]
        [StringLength(200)]
        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        [Required]
        public decimal? Price { get; set; }

        [Required]
        [StringLength(25)]
        public string User { get; set; }
    }

    public class UpdatePriceRequestModel
    {
        [Required]
        public decimal Price { get; set; }

        [Required]
        [StringLength(25)]
        public string User { get; set; }
    }

    public class LikeProductRequestModel
    {
        [Required]
        [StringLength(25)]
        public string User { get; set; }
    }

    public class PlaceOrderRequestModel
    {
        [Required]
        [StringLength(25)]
        public string User { get; set; }

        public IEnumerable<PlaceOrderDetailRequestModel> Details { get; set; }
    }

    public class PlaceOrderDetailRequestModel
    {
        [Required]
        public int? ProductID { get; set; }

        [Required]
        public int? Quantity { get; set; }
    }

    public static class RequestsExtensions
    {
        public static Product ToEntity(this AddProductRequestModel requestModel)
        {
            return new Product
            {
                ProductID = requestModel.ProductID,
                ProductName = requestModel.ProductName,
                ProductDescription = requestModel.ProductDescription,
                Price = requestModel.Price,
                CreationUser = requestModel.User
            };
        }

        public static AddProductRequestModel ToRequestModel(this Product entity)
        {
            return new AddProductRequestModel
            {
                ProductID = entity.ProductID,
                ProductName = entity.ProductName,
                ProductDescription = entity.ProductDescription,
                Price = entity.Price,
                User = entity.CreationUser
            };
        }
    }
}
