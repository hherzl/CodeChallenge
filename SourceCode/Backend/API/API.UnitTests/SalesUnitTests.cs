using System.Collections.Generic;
using System.Threading.Tasks;
using API.Controllers;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace API.UnitTests
{
    public class SalesUnitTests
    {
        [Fact]
        public async Task TestPlaceOrderAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetCodeChallengeDbContext(nameof(TestPlaceOrderAsync));
            var warehouseRepository = RepositoryMocker.GetWarehouseRepository(dbContext);
            var salesRepository = RepositoryMocker.GetSalesRepository(dbContext);
            var controller = new SalesController(warehouseRepository, salesRepository, null);
            var request = new PlaceOrderRequest
            {
                User = "unittests",
                Details = new List<PlaceOrderDetailRequest>
                {
                    new PlaceOrderDetailRequest
                    {
                        ProductID = 1,
                        Quantity = 1
                    }
                }
            };

            // Act
            var response = await controller.PlaceOrderAsync(request) as ObjectResult;
            var value = response.Value as ISingleResponse<PlaceOrderRequest>;

            // Assert
            Assert.False(value.DidError);
        }
    }
}
