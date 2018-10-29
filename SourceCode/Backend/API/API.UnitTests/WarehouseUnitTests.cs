using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using API.Core.EntityLayer.Warehouse;
using API.Models;
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
            var repository = RepositoryMocker.GetWarehouseRepository(nameof(TestGetProductsAsync));
            var controller = new WarehouseController(repository, null);

            // Act
            var response = await controller.GetProductsAsync() as ObjectResult;
            var value = response.Value as IPagedResponse<Product>;

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Count() > 0);
        }

        [Fact]
        public async Task TestAddProductAsync()
        {
            // Arrange
            var repository = RepositoryMocker.GetWarehouseRepository(nameof(TestAddProductAsync));
            var controller = new WarehouseController(repository, null);
            var request = new AddProductRequest
            {
                ProductID = 20,
                ProductName = "Coca Cola Zero 24 fl Oz Bottle",
                ProductDescription = "Enjoy Coca-Cola’s crisp, delicious taste with meals, on the go, or to share. Serve ice cold for maximum refreshment.",
                Price = 2.15m,
                User = "seed"
            };

            // Act
            var response = await controller.AddProductAsync(request) as ObjectResult;
            var value = response.Value as ISingleResponse<AddProductRequest>;

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestUpdatePriceAsync()
        {
            // Arrange
            var repository = RepositoryMocker.GetWarehouseRepository(nameof(TestUpdatePriceAsync));
            var controller = new WarehouseController(repository, null);
            var id = 1;
            var request = new UpdatePriceRequestModel
            {
                Price = 2.15m,
                User = "seed"
            };

            // Act
            var response = await controller.UpdatePriceAsync(id, request) as ObjectResult;
            var value = response.Value as ISingleResponse<UpdatePriceRequestModel>;

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestLikeProductAsync()
        {
            // Arrange
            var repository = RepositoryMocker.GetWarehouseRepository(nameof(TestLikeProductAsync));
            var controller = new WarehouseController(repository, null);
            var id = 1;
            var request = new LikeProductRequest
            {
                User = "reviewer"
            };

            // Act
            var response = await controller.LikeProductAsync(id, request) as ObjectResult;
            var value = response.Value as ISingleResponse<LikeProductRequest>;

            // Assert
            Assert.False(value.DidError);
        }
    }
}
