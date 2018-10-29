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
    }
}
