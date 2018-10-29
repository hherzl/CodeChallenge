using System;
using System.Linq;
using System.Threading.Tasks;
using API.Core.DataLayer.Contracts;
using API.Core.DataLayer.Repositories;
using API.Core.EntityLayer.Warehouse;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WarehouseController : ControllerBase
    {
        protected readonly IWarehouseRepository Repository;
        protected ILogger Logger;

        public WarehouseController(IWarehouseRepository repository, ILogger<WarehouseController> logger)
        {
            Repository = repository;
            Logger = logger;
        }

        [HttpGet("Product")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductsAsync(int? pageSize = 10, int? pageNumber = 1, string name = "", string sortBy = "")
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetProductsAsync));

            var response = new PagedResponse<Product>();

            try
            {
                // Get query from repository
                var query = Repository.GetProducts(name);

                // Sorting list
                if (sortBy == "popularity")
                    query = query.OrderByDescending(item => item.Likes);
                else
                    query = query.OrderBy(item => item.ProductName);

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
                response.SetError(Logger, nameof(GetProductsAsync), ex);
            }

            return response.ToHttpResponse();
        }

        [HttpPost("Product")]
        public async Task<IActionResult> AddProductAsync([FromBody]AddProductRequest requestModel)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(AddProductAsync));

            // Validate request model
            if (!ModelState.IsValid)
                return BadRequest(requestModel);

            var response = new SingleResponse<AddProductRequest>();

            try
            {
                var entity = requestModel.ToEntity();

                // Set default values
                entity.Likes = 0;
                entity.Stocks = 0;
                entity.Available = true;

                // Check if entity exists
                var existingProduct = await Repository.GetProductByProductNameAsync(entity);

                if (existingProduct != null)
                {
                    ModelState.AddModelError("ProductName", "Product name already exists");
                    return BadRequest(ModelState);
                }

                // Add entity to database
                Repository.Add(entity);

                await Repository.CommitChangesAsync();

                response.Model = entity.ToAddProductRequest();

                Logger?.LogInformation("The entity was created successfully.");
            }
            catch (Exception ex)
            {
                response.SetError(Logger, nameof(AddProductAsync), ex);
            }

            return response.ToHttpResponse();
        }

        [HttpPut("Product/{id}")]
        public async Task<IActionResult> UpdatePriceAsync(int id, [FromBody]UpdatePriceRequestModel requestModel)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(UpdatePriceAsync));

            // Validate request model
            if (!ModelState.IsValid)
                return BadRequest(requestModel);

            var response = new SingleResponse<UpdatePriceRequestModel>();

            try
            {
                // Get entity by id
                var entity = await Repository.GetProductAsync(new Product(id));

                if (entity == null)
                    return NotFound();

                // Set changes
                entity.Price = requestModel.Price;
                entity.LastUpdateUser = requestModel.User;
                entity.LastUpdateDateTime = DateTime.Now;

                // Update entity to database
                Repository.Update(entity);

                await Repository.CommitChangesAsync();

                response.Message = "The price for product was changed successfully.";

                Logger?.LogInformation(response.Message);

                // Add product price to history
                var history = new ProductPriceHistory
                {
                    ProductID = entity.ProductID,
                    Price = entity.Price,
                    StartDate = DateTime.Now,
                    CreationUser = requestModel.User,
                    CreationDateTime = DateTime.Now
                };

                Repository.Add(history);

                await Repository.CommitChangesAsync();

                Logger?.LogInformation("The price for product was saved in history successfully.");
            }
            catch (Exception ex)
            {
                response.SetError(Logger, nameof(UpdatePriceAsync), ex);
            }

            return response.ToHttpResponse();
        }

        [HttpPut("LikeProduct/{id}")]
        public async Task<IActionResult> LikeProductAsync(int id, [FromBody]LikeProductRequest requestModel)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(LikeProductAsync));

            // Validate request model
            if (!ModelState.IsValid)
                return BadRequest(requestModel);

            var response = new SingleResponse<LikeProductRequest>();

            try
            {
                // Get entity by id
                var entity = await Repository.GetProductAsync(new Product(id));

                if (entity == null)
                    return NotFound();

                // Set changes
                entity.Likes += 1;
                entity.LastUpdateUser = requestModel.User;
                entity.LastUpdateDateTime = DateTime.Now;

                // Update entity to database
                Repository.Update(entity);

                await Repository.CommitChangesAsync();

                response.Model = new LikeProductRequest
                {
                    User = requestModel.User
                };

                response.Message = string.Format("The product '{0}' has a new like, user: '{1}'.", entity.ProductName, response.Model.User);

                Logger?.LogInformation(response.Message);
            }
            catch (Exception ex)
            {
                response.SetError(Logger, nameof(LikeProductAsync), ex);
            }

            return response.ToHttpResponse();
        }
    }
}
