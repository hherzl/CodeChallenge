using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using API.Core.EntityLayer.Warehouse;
using API.Models;
using API.UnitTests.Mockers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace API.UnitTests
{
    public class WarehouseUnitTests
    {
        [Fact]
        public async Task TestGetProductsAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetStoreDbContext(nameof(TestGetProductsAsync));
            var service = ServiceMocker.GetWarehouseService(dbContext);
            var controller = new WarehouseController(service, null);

            // Act
            var response = await controller.GetProductsAsync() as ObjectResult;
            var value = response.Value as IPagedResponse<Product>;

            service.Dispose();

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Count() > 0);
        }

        [Fact]
        public async Task TestAddProductAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetStoreDbContext(nameof(TestAddProductAsync));
            var service = ServiceMocker.GetWarehouseService(dbContext);
            var controller = new WarehouseController(service, null);

            var request = new AddProductRequest
            {
                ProductID = 100,
                ProductName = "Coca Cola Zero 24 fl Oz Bottle Special Edition",
                ProductDescription = "Enjoy Coca-Cola’s crisp.",
                Price = 2.15m
            };

            controller.MockControllerContext();

            // Act
            var response = await controller.AddProductAsync(request) as ObjectResult;
            var value = response.Value as ISingleResponse<AddProductRequest>;

            service.Dispose();

            // Assert
            Assert.False(value?.DidError);
        }

        [Fact]
        public async Task TestUpdateProductPriceAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetStoreDbContext(nameof(TestUpdateProductPriceAsync));
            var service = ServiceMocker.GetWarehouseService(dbContext);
            var controller = new WarehouseController(service, null);
            var id = 1;
            var request = new UpdateProductPriceRequest
            {
                Price = 2.15m
            };

            controller.MockControllerContext();

            // Act
            var response = await controller.UpdateProductPriceAsync(id, request) as ObjectResult;
            var value = response.Value as ISingleResponse<UpdateProductPriceRequest>;

            service.Dispose();

            // Assert
            Assert.False(value?.DidError);
        }

        [Fact]
        public async Task TestLikeProductAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetStoreDbContext(nameof(TestLikeProductAsync));
            var service = ServiceMocker.GetWarehouseService(dbContext);
            var controller = new WarehouseController(service, null);
            var id = 1;
            var request = new LikeProductRequest();

            controller.MockControllerContext();

            // Act
            var response = await controller.LikeProductAsync(id, request) as ObjectResult;
            var value = response.Value as ISingleResponse<LikeProductRequest>;

            service.Dispose();

            // Assert
            Assert.False(value?.DidError);
        }

        [Fact]
        public async Task TestDeleteProductAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetStoreDbContext(nameof(TestDeleteProductAsync));
            var service = ServiceMocker.GetWarehouseService(dbContext);
            var controller = new WarehouseController(service, null);
            var id = 1;

            controller.MockControllerContext();

            // Act
            var response = await controller.DeleteProductAsync(id) as ObjectResult;
            var value = response.Value as IResponse;

            service.Dispose();

            // Assert
            Assert.False(value?.DidError);
        }
    }
}
