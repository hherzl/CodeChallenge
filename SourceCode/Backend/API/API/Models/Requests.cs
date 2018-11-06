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

        [StringLength(25)]
        public string User { get; set; }
    }

    public class UpdateProductPriceRequest
    {
        [Required]
        public decimal Price { get; set; }
    }

    public class PlaceOrderRequest
    {
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
        public static Product ToEntity(this AddProductRequest request)
            => new Product
            {
                ProductID = request.ProductID,
                ProductName = request.ProductName,
                ProductDescription = request.ProductDescription,
                Price = request.Price,
                CreationUser = request.User
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
