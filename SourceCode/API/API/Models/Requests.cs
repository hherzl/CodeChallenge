using System;
using System.ComponentModel.DataAnnotations;
using API.Core.EntityLayer.Warehouse;

namespace API.Models
{
    public class AddProductRequestModel
    {
        [Key]
        public Int32? ProductID { get; set; }

        [Required]
        [StringLength(200)]
        public String ProductName { get; set; }

        public String ProductDescription { get; set; }

        [Required]
        public Decimal? Price { get; set; }

        [Required]
        [StringLength(25)]
        public String User { get; set; }
    }

    public class UpdatePriceRequestModel
    {
        [Required]
        public Decimal? Price { get; set; }

        [Required]
        [StringLength(25)]
        public String User { get; set; }
    }

    public class LikeProductRequestModel
    {
        [Required]
        [StringLength(25)]
        public String User { get; set; }
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
