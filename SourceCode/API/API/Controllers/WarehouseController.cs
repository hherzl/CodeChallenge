using System;
using System.Threading.Tasks;
using API.Core.DataLayer.Contracts;
using API.Core.DataLayer.Repositories;
using API.Core.EntityLayer.Warehouse;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        protected readonly IWarehouseRepository Repository;
        protected ILogger Logger;

        public WarehouseController(IWarehouseRepository repository, ILogger<WarehouseController> logger)
        {
            Repository = repository;
            Logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync(int? pageSize = 10, int? pageNumber = 1)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetProductsAsync));

            var response = new PagedResponse<Product>();

            try
            {
                // Get query from repository
                var query = Repository.GetProducts();

                // Set paging's information
                response.PageSize = (int)pageSize;
                response.PageNumber = (int)pageNumber;
                response.ItemsCount = await query.CountAsync();

                // Retrieve items by page size and page number, set model for response
                response.Model = await query.Paging(response.PageSize, response.PageNumber).ToListAsync();

                response.Message = string.Format("Page {0} of {1}, Total of rows: {2}.", pageNumber, response.PageCount, response.ItemsCount);

                Logger?.LogInformation(response.Message);
            }
            catch (Exception ex)
            {
                response.SetError(Logger, ex);
            }

            return response.ToHttpResponse();
        }

        [HttpPost("Product")]
        public async Task<IActionResult> PostProductAsync([FromBody]ProductRequestModel requestModel)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(PostProductAsync));

            // Validate request model
            if (!ModelState.IsValid)
                return BadRequest(requestModel);

            var response = new SingleResponse<ProductRequestModel>();

            try
            {
                var entity = requestModel.ToEntity();

                // Set default values
                entity.Likes = 0;
                entity.Stocks = 0;
                entity.Available = true;

                // Check if entity exists
                if ((await Repository.GetProductByProductNameAsync(entity)) != null)
                    return BadRequest();

                // Add entity to database
                Repository.Add(entity);

                await Repository.CommitChangesAsync();

                response.Model = entity.ToRequestModel();

                Logger?.LogInformation("The entity was created successfully.");
            }
            catch (Exception ex)
            {
                response.SetError(Logger, ex);
            }

            return response.ToHttpResponse();
        }
    }
}
