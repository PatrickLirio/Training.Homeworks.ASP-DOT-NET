

using Microsoft.AspNetCore.Mvc;
using MyShop.DTO;
using MyShop.Services.Interfaces;

namespace MyShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<IActionResult> GetProductLists()
        {
            var products = await _productService.GetAllProducts(); 
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
          try
            {
                var product = await _productService.GetProductById(id);
                return Ok(product);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            try
            {
                var product = await _productService.GetProductByName(name);
                return Ok(product);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetProductsByCategory(string category)
        {
            var products = await _productService.GetProductsByCategory(category);
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductCreateDTO productInput)
        {
            try
            {
                await _productService.AddProduct(productInput);
                // return CreatedAtAction(nameof(GetProductById), new { id = productInput.Id }, productInput);
                return Created();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDTO productInput)
        {
            try
            {
                await _productService.UpdateProduct(id, productInput);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
         }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProduct(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
         }

    }
}