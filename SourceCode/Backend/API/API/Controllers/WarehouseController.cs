using System;
using System.Linq;
using System.Threading.Tasks;
using API.Core.BusinessLayer;
using API.Core.DataLayer.Repositories;
using API.Core.EntityLayer.Warehouse;
using API.Extensions;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    /// <summary>
    /// Contains all operations related to Warehouse feature
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WarehouseController : ControllerBase
    {
        protected readonly IWarehouseService Service;
        protected ILogger Logger;

        public WarehouseController(IWarehouseService warehouseService, ILogger<WarehouseController> logger)
        {
            Service = warehouseService;
            Logger = logger;
        }

        /// <summary>
        /// Gets the products from store
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Page number</param>
        /// <param name="name">Product name</param>
        /// <param name="sortBy">Sort by popularity</param>
        /// <returns>A list of product according to criteria</returns>
        [HttpGet("Product")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductsAsync(int? pageSize = 10, int? pageNumber = 1, string name = "", string sortBy = "")
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetProductsAsync));

            var response = new PagedResponse<Product>();

            try
            {
                // Get query from repository
                var query = Service.WarehouseRepository.GetProducts(name);

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

        /// <summary>
        /// Adds a new product
        /// </summary>
        /// <param name="request">Request for add product</param>
        /// <returns>A single response with new product info</returns>
        [HttpPost("Product")]
        public async Task<IActionResult> AddProductAsync([FromBody]AddProductRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(AddProductAsync));

            // Validate request model
            if (!ModelState.IsValid)
                return BadRequest(request);

            var response = new SingleResponse<AddProductRequest>();

            try
            {
                var entity = request.ToEntity();

                // Set default values
                entity.Likes = 0;
                entity.Stocks = 0;
                entity.Available = true;

                // Check if entity exists
                var existingProduct = await Service.WarehouseRepository.GetProductByProductNameAsync(entity);

                if (existingProduct != null)
                {
                    ModelState.AddModelError("ProductName", "Product name already exists");
                    return BadRequest(ModelState);
                }

                entity.CreationUser = User.GetClientName();

                // Add entity to database
                Service.WarehouseRepository.Add(entity);

                await Service.WarehouseRepository.CommitChangesAsync();

                response.Model = entity.ToAddProductRequest();

                Logger?.LogInformation("The entity was created successfully.");
            }
            catch (Exception ex)
            {
                response.SetError(Logger, nameof(AddProductAsync), ex);
            }

            return response.ToHttpResponse();
        }

        /// <summary>
        /// Updates the price for existing product
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <param name="request">Request for update price</param>
        /// <returns>A single response for product price update</returns>
        [HttpPut("Product/{id}")]
        public async Task<IActionResult> UpdatePriceAsync(int id, [FromBody]UpdatePriceRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(UpdatePriceAsync));

            // Validate request model
            if (!ModelState.IsValid)
                return BadRequest(request);

            var response = new SingleResponse<UpdatePriceRequest>();

            try
            {
                // Get entity by id
                var entity = await Service.WarehouseRepository.GetProductAsync(new Product(id));

                if (entity == null)
                    return NotFound();

                // Set changes
                entity.Price = request.Price;
                entity.LastUpdateUser = User.GetClientName();

                // Update entity to database
                Service.WarehouseRepository.Update(entity);

                await Service.WarehouseRepository.CommitChangesAsync();

                response.Message = "The price for product was changed successfully.";

                Logger?.LogInformation(response.Message);

                // Add product price to history
                var history = new ProductPriceHistory
                {
                    ProductID = entity.ProductID,
                    Price = entity.Price,
                    StartDate = DateTime.Now,
                    CreationUser = User.GetClientName()
                };

                Service.WarehouseRepository.Add(history);

                await Service.WarehouseRepository.CommitChangesAsync();

                Logger?.LogInformation("The price for product was saved in history successfully.");
            }
            catch (Exception ex)
            {
                response.SetError(Logger, nameof(UpdatePriceAsync), ex);
            }

            return response.ToHttpResponse();
        }

        /// <summary>
        /// Likes an existing product
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <param name="request">Request model for </param>
        /// <returns>A single response as result of like product</returns>
        [HttpPut("LikeProduct/{id}")]
        public async Task<IActionResult> LikeProductAsync(int id, [FromBody]LikeProductRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(LikeProductAsync));

            // Validate request model
            if (!ModelState.IsValid)
                return BadRequest(request);

            var response = new SingleResponse<LikeProductRequest>();

            try
            {
                // Get entity by id
                var entity = await Service.WarehouseRepository.GetProductAsync(new Product(id));

                if (entity == null)
                    return NotFound();

                // Set changes
                entity.Likes += 1;
                entity.LastUpdateUser = User.GetClientName();

                // Update entity to database
                Service.WarehouseRepository.Update(entity);

                await Service.WarehouseRepository.CommitChangesAsync();

                response.Model = new LikeProductRequest
                {
                    User = entity.LastUpdateUser
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
