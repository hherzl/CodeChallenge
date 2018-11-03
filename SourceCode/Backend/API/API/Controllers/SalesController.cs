using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core.DataLayer.Contracts;
using API.Core.EntityLayer.Sales;
using API.Core.EntityLayer.Warehouse;
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
        protected readonly IWarehouseRepository WarehouseRepository;
        protected readonly ISalesRepository SalesRepository;
        protected ILogger Logger;

        public SalesController(IWarehouseRepository warehouseRepository, ISalesRepository salesRepository, ILogger<WarehouseController> logger)
        {
            WarehouseRepository = warehouseRepository;
            SalesRepository = salesRepository;
            Logger = logger;
        }

        /// <summary>
        /// Places a new order
        /// </summary>
        /// <param name="requestModel">Order request</param>
        /// <returns>A single response as for order creation</returns>
        [HttpPost("PlaceOrder")]
        public async Task<IActionResult> PlaceOrderAsync([FromBody]PlaceOrderRequest requestModel)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(PlaceOrderAsync));

            // Validate request model
            if (!ModelState.IsValid)
                return BadRequest(requestModel);

            var response = new SingleResponse<PlaceOrderRequest>();

            try
            {
                var header = new OrderHeader
                {
                    OrderDate = DateTime.Now,
                    Total = 0m,
                    CreationUser = User.GetClientName()
                };

                var orderDetails = new List<OrderDetail>();

                foreach (var detail in requestModel.Details)
                {
                    var product = await WarehouseRepository.GetProductAsync(new Product(detail.ProductID));

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

                header.Total = orderDetails.Sum(item => item.Total);

                SalesRepository.Add(header);

                await SalesRepository.CommitChangesAsync();

                foreach (var item in orderDetails)
                {
                    item.OrderHeaderID = header.OrderHeaderID;

                    SalesRepository.Add(item);
                }

                await SalesRepository.CommitChangesAsync();

                foreach (var detail in requestModel.Details)
                {
                    var product = await WarehouseRepository.GetProductAsync(new Product(detail.ProductID));

                    product.Stocks -= 1;
                }

                await SalesRepository.CommitChangesAsync();

                response.Message = string.Format("The order was placed successfully, ID: {0}", header.OrderHeaderID);

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
