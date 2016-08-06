using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Data.Models;
using Newtonsoft.Json.Linq;
using refactor_me.Services;
using refactor_me.Validations;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Route]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            if (products.Any())
            {
                return Ok(JObject.FromObject(new { Items = products }));
            }
            return NotFound();
        }

        [Route]
        [HttpGet]
        public async Task<IHttpActionResult> SearchProductsByName(string name)
        {
            var products = await _productService.SearchProductsByNameAsync(name);
            if (products.Any())
            {
                return Ok(products);
            }
            return NotFound();
        }

        [Route("{productId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetProductByProductId(Guid productId)
        {
            var product = await _productService.GetProductByProductIdAsync(productId);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }

        [Route]
        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> CreateProduct(Product product)
        {
            //Validate create product request
            var validationResult = new CreateUpdateProductValidator().Validate(product);
            if (!validationResult.IsValid)
                return BadRequest();

            var newProduct = await _productService.CreateProductAsync(product);
            if (newProduct != null)
            {
                return Ok(product);
            }
            return InternalServerError();
        }

        [Route("{id}")]
        [HttpPut]
        [Authorize]
        public async Task<IHttpActionResult> UpdateProduct(Product product)
        {
            //Validate update product request
            var validationResult = new CreateUpdateProductValidator().Validate(product);
            if (!validationResult.IsValid)
                return BadRequest();

            var modifiedProduct = await _productService.UpdateProductAsync(product);
            if (modifiedProduct != null)
            {
                return Ok(modifiedProduct);
            }
            return InternalServerError();
        }

        [Route("{productId}")]
        [HttpDelete]
        [Authorize]
        public async Task<IHttpActionResult> DeleteProduct(Guid productId)
        {
            var result = await _productService.DeleteProductAsync(productId);
            if (result)
            {
                return Ok();
            }
            return InternalServerError();
        }

        [Route("{productId}/options")]
        [HttpGet]
        public async Task<IHttpActionResult> GetProductOptions(Guid productId)
        {
            var productOptions = await _productService.GetAllProductOptionsAsync(productId);
            if (productOptions.Any())
            {
                return Ok(JObject.FromObject(new { Items = productOptions }));
            }
            return NotFound();
        }

        [Route("{productId}/options/{optionId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetProductOption(Guid productId, Guid optionId)
        {
            var productOption = await _productService.GetProductOptionAsync(productId, optionId);
            if (productOption != null)
            {
                return Ok(productOption);
            }
            return NotFound();
        }

        [Route("{productId}/options")]
        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> CreateProductOption(Guid productId, ProductOption productOption)
        {
            productOption.ProductId = productId;

            //Validate create product option request
            var validationResult = new CreateUpdateProductOptionValidator().Validate(productOption);
            if (!validationResult.IsValid)
                return BadRequest();

            var newProductOption = await _productService.CreateProductOptionAsync(productOption);
            if (newProductOption != null)
            {
                return Ok(newProductOption);
            }
            return InternalServerError();
        }

        [Route("{productId}/options/{optionId}")]
        [HttpPut]
        [Authorize]
        public async Task<IHttpActionResult> UpdateProductOption(Guid productId, Guid optionId,
            ProductOption productOption)
        {
            productOption.ProductId = productId;
            productOption.Id = optionId;

            //Validate update product option request
            var validationResult = new CreateUpdateProductOptionValidator().Validate(productOption);
            if (!validationResult.IsValid)
                return BadRequest();

            var modifiedProductOption = await _productService.UpdateProductOptionAsync(productOption);
            if (modifiedProductOption != null)
            {
                return Ok(modifiedProductOption);
            }
            return InternalServerError();
        }

        [Route("{productId}/options/{optionId}")]
        [HttpDelete]
        [Authorize]
        public async Task<IHttpActionResult> DeleteProductOption(Guid productId, Guid optionId)
        {
            var result = await _productService.DeleteProductOptionAsync(productId, optionId);
            if (result)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}