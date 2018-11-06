using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.BusinessLayer;
using API.Core.EntityLayer.Sales;
using API.Core.EntityLayer.Warehouse;
using API.Extensions;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    /// <summary>
    /// Contains all operations related to Sales feature
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SalesController : ControllerBase
    {
        protected readonly ISalesService SalesService;
        protected ILogger Logger;

        public SalesController(ISalesService salesService, ILogger<WarehouseController> logger)
        {
            SalesService = salesService;
            Logger = logger;
        }

        /// <summary>
        /// Places a new order
        /// </summary>
        /// <param name="request">Order request</param>
        /// <returns>A response as result of place new order</returns>
        /// <response code="200">If creation of order it was succes</response>
        /// <response code="400">For bad request</response>
        /// <response code="401">For unauthorized clients</response>
        /// <response code="403">If client doesn't have rights to place order</response>
        /// <response code="500">If there was an error</response>
        [Authorize(Policy = "CustomerPolicy")]
        [HttpPost("PlaceOrder")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PlaceOrderAsync([FromBody]PlaceOrderRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(PlaceOrderAsync));

            // Validate request model
            if (!ModelState.IsValid)
                return BadRequest(request);

            var response = new Response();

            try
            {
                var orderDetails = new List<OrderDetail>();

                foreach (var detail in request.Details)
                {
                    var product = await SalesService.WarehouseRepository.GetProductAsync(new Product(detail.ProductID));

                    if (product == null)
                    {
                        ModelState.AddModelError("ProductID", "There is a non existing product in order detail");
                        return BadRequest(ModelState);
                    }

                    if (product.Available == false)
                    {
                        ModelState.AddModelError("ProductID", "There is a discontinued product in order detail");
                        return BadRequest(ModelState);
                    }

                    if (detail.Quantity <= 0)
                    {
                        ModelState.AddModelError("Quantity", string.Format("The quantity for product: '{0}' must be greater than zero, current value: '{1}'.", detail.ProductID, detail.Quantity));
                        return BadRequest(ModelState);
                    }

                    orderDetails.Add(new OrderDetail
                    {
                        ProductID = product.ProductID,
                        ProductName = product.ProductName,
                        UnitPrice = product.Price,
                        Quantity = detail.Quantity,
                        Total = product.Price * detail.Quantity,
                        CreationUser = User.GetClientName()
                    });
                }

                var header = new OrderHeader
                {
                    CreationUser = User.GetClientName()
                };

                await SalesService.PlaceOrderAsync(header, orderDetails);

                response.Message = string.Format("The order was placed successfully, ID: {0}.", header.OrderHeaderID);

                Logger?.LogInformation(response.Message);
            }
            catch (Exception ex)
            {
                response.SetError(Logger, nameof(PlaceOrderAsync), ex);
            }

            return response.ToHttpResponse();
        }
    }
}
