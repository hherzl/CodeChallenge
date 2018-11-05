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
        /// <response code="200">A success response with products list</response>
        /// <response code="204">If there are not products</response>
        /// <response code="500">If there was an error</response>
        [AllowAnonymous]
        [HttpGet("Product")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetProductsAsync(int? pageSize = 10, int? pageNumber = 1, string name = "", string sortBy = "popularity")
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

                response.Message = string.Format("Page {0} of {1}, Total of products: {2}.", pageNumber, response.PageCount, response.ItemsCount);

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
        /// <response code="200">Returns the newly created product</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an error</response>
        [Authorize(Policy = "AdministratorPolicy")]
        [HttpPost("Product")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
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

                // Check if entity exists
                var existingProduct = await Service.WarehouseRepository.GetProductByProductNameAsync(entity);

                if (existingProduct != null)
                {
                    ModelState.AddModelError("ProductName", "Product name already exists");
                    return BadRequest(ModelState);
                }

                entity.CreationUser = User.GetClientName();

                await Service.CreateProductAsync(entity);

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
        /// <response code="200">If update for product price it was success</response>
        /// <response code="400">For bad request</response>
        /// <response code="404">If product not exists</response>
        /// <response code="500">If there was an error</response>
        [Authorize(Policy = "AdministratorPolicy")]
        [HttpPut("UpdateProductPrice/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateProductPriceAsync(int id, [FromBody]UpdateProductPriceRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(UpdateProductPriceAsync));

            // Validate request model
            if (!ModelState.IsValid)
                return BadRequest(request);

            var response = new SingleResponse<UpdateProductPriceRequest>();

            try
            {
                // Get entity by id
                var entity = await Service.WarehouseRepository.GetProductAsync(new Product(id));

                if (entity == null)
                    return NotFound();

                entity.LastUpdateUser = User.GetClientName();

                await Service.UpdatePriceProductAsync(entity);

                response.Message = string.Format("The price for product: {0} was changed successfully.", entity.ProductID);

                Logger?.LogInformation("The price was saved successfully.");

                Logger?.LogInformation("The price for product was saved in history successfully.");
            }
            catch (Exception ex)
            {
                response.SetError(Logger, nameof(UpdateProductPriceAsync), ex);
            }

            return response.ToHttpResponse();
        }

        /// <summary>
        /// Likes an existing product
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <param name="request">Request model for </param>
        /// <returns>A single response as result of like product</returns>
        /// <response code="200">If like for product it was success</response>
        /// <response code="400">For bad request</response>
        /// <response code="404">If product not exists</response>
        /// <response code="500">If there was an error</response>
        [Authorize(Policy = "CustomerPolicy")]
        [HttpPut("LikeProduct/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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

                entity.LastUpdateUser = User.GetClientName();

                await Service.LikeProductAsync(entity);

                response.Model = new LikeProductRequest { User = entity.LastUpdateUser };

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
