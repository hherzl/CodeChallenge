using System;
using System.ComponentModel.DataAnnotations;
using API.Core.EntityLayer.Warehouse;

namespace API.Models
{
    public class ProductRequestModel
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
        public String CreationUser { get; set; }
    }

    public static class RequestsExtensions
    {
        public static Product ToEntity(this ProductRequestModel requestModel)
        {
            return new Product
            {
                ProductID = requestModel.ProductID,
                ProductName = requestModel.ProductName,
                ProductDescription = requestModel.ProductDescription,
                Price = requestModel.Price,
                CreationUser = requestModel.CreationUser
            };
        }

        public static ProductRequestModel ToRequestModel(this Product entity)
        {
            return new ProductRequestModel
            {
                ProductID = entity.ProductID,
                ProductName = entity.ProductName,
                ProductDescription = entity.ProductDescription,
                Price = entity.Price,
                CreationUser = entity.CreationUser
            };
        }
    }
}
