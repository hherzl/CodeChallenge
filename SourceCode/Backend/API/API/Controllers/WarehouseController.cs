using System;
using System.Linq;
using System.Threading.Tasks;
using API.Core.BusinessLayer;
using API.Core.DataLayer;
using API.Core.EntityLayer.Warehouse;
using API.Extensions;
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
                var query = Service.DbContext.GetProducts(name);

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
        /// <response code="401">For unauthorized clients</response>
        /// <response code="403">If client doesn't have rights to add product</response>
        /// <response code="500">If there was an error</response>
        [Authorize(Policy = "AdministratorPolicy")]
        [HttpPost("Product")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
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
                var existingProduct = await Service.DbContext.GetProductByProductNameAsync(entity);

                if (existingProduct != null)
                {
                    ModelState.AddModelError("ProductName", "Product name already exists");

                    return BadRequest(ModelState);
                }

                entity.CreationUser = User.GetUserName();

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
        /// <param name="request">Request for update product price</param>
        /// <returns>A response as result of update product price</returns>
        /// <response code="200">If product price update it was success</response>
        /// <response code="400">For bad request</response>
        /// <response code="401">For unauthorized clients</response>
        /// <response code="403">If client doesn't have rights to update product price</response>
        /// <response code="404">If product not exists</response>
        /// <response code="500">If there was an error</response>
        [Authorize(Policy = "AdministratorPolicy")]
        [HttpPut("UpdateProductPrice/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateProductPriceAsync(int id, [FromBody]UpdateProductPriceRequest request)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(UpdateProductPriceAsync));

            // Validate request model
            if (!ModelState.IsValid)
                return BadRequest(request);

            var response = new Response();

            try
            {
                // Get entity by id
                var entity = await Service.DbContext.GetProductAsync(new Product(id));

                if (entity == null)
                    return NotFound();

                entity.LastUpdateUser = User.GetUserName();

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
        /// <returns>A single response as result of like product</returns>
        /// <response code="200">If like for product it was success</response>
        /// <response code="401">For unauthorized clients</response>
        /// <response code="403">If client doesn't have rights to like product</response>
        /// <response code="404">If product not exists</response>
        /// <response code="500">If there was an error</response>
        [Authorize(Policy = "CustomerPolicy")]
        [HttpPut("LikeProduct/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> LikeProductAsync(int id)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(LikeProductAsync));

            var response = new Response();

            try
            {
                // Get entity by id
                var entity = await Service.DbContext.GetProductAsync(new Product(id));

                if (entity == null)
                    return NotFound();

                entity.LastUpdateUser = User.GetUserName();

                var affectedRows = await Service.LikeProductAsync(entity);

                if (affectedRows == 0)
                {
                    response.Message = string.Format("The product '{0}' has a new like, user: '{1}'.", entity.ProductName, entity.LastUpdateUser);

                    Logger?.LogInformation(response.Message);
                }
                if (affectedRows == 0)
                {
                    response.Message = string.Format("The product '{0}' already have a like from '{1}' user.", entity.ProductName, entity.LastUpdateUser);

                    Logger?.LogInformation(response.Message);
                }
            }
            catch (Exception ex)
            {
                response.SetError(Logger, nameof(LikeProductAsync), ex);
            }

            return response.ToHttpResponse();
        }

        /// <summary>
        /// Deletes an existing product
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>A response as result of delete product</returns>
        /// <response code="200">If deleting product it was success</response>
        /// <response code="401">For unauthorized clients</response>
        /// <response code="403">If client doesn't have rights to delete product</response>
        /// <response code="404">If product not exists</response>
        /// <response code="500">If there was an error</response>
        [Authorize(Policy = "AdministratorPolicy")]
        [HttpDelete("Product/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(DeleteProductAsync));

            var response = new Response();

            try
            {
                // Get entity by id
                var entity = await Service.DbContext.GetProductAsync(new Product(id));

                if (entity == null)
                    return NotFound();

                var affectedRows = await Service.DeleteProductAsync(entity);

                if (affectedRows > 0)
                {
                    response.Message = "The product was deleted successfully";
                }
            }
            catch (Exception ex)
            {
                response.SetError(Logger, nameof(DeleteProductAsync), ex);
            }

            return response.ToHttpResponse();
        }
    }
}
