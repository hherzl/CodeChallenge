using System.Collections.Generic;
using System.Threading.Tasks;
using API.Controllers;
using API.Models;
using API.UnitTests.Mockers;
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
            var dbContext = DbContextMocker.GetStoreDbContext(nameof(TestPlaceOrderAsync));
            var service = ServiceMocker.GetSalesService(dbContext);
            var controller = new SalesController(service, null);
            var request = new PlaceOrderRequest
            {
                Details = new List<PlaceOrderDetailRequest>
                {
                    new PlaceOrderDetailRequest
                    {
                        ProductID = 1000,
                        Quantity = 1
                    }
                }
            };

            controller.MockControllerContext();

            // Act
            var response = await controller.PlaceOrderAsync(request) as ObjectResult;
            var value = response.Value as IResponse;

            service.Dispose();

            // Assert
            Assert.False(value?.DidError);
        }
    }
}
