using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using API.Core.EntityLayer.Warehouse;

namespace API.Models
{
    public class AddProductRequest
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

    public class LikeProductRequest
    {
        [Required]
        [StringLength(25)]
        public string User { get; set; }
    }

    public class PlaceOrderRequest
    {
        [Required]
        [StringLength(25)]
        public string User { get; set; }

        public IEnumerable<PlaceOrderDetailRequest> Details { get; set; }
    }

    public class PlaceOrderDetailRequest
    {
        [Required]
        public int? ProductID { get; set; }

        [Required]
        public int? Quantity { get; set; }
    }

    public static class RequestsExtensions
    {
        public static Product ToEntity(this AddProductRequest requestModel)
            => new Product
            {
                ProductID = requestModel.ProductID,
                ProductName = requestModel.ProductName,
                ProductDescription = requestModel.ProductDescription,
                Price = requestModel.Price,
                CreationUser = requestModel.User
            };

        public static AddProductRequest ToAddProductRequest(this Product entity)
            => new AddProductRequest
            {
                ProductID = entity.ProductID,
                ProductName = entity.ProductName,
                ProductDescription = entity.ProductDescription,
                Price = entity.Price,
                User = entity.CreationUser
            };
    }
}
