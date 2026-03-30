

using Microsoft.AspNetCore.Mvc;
using MyShop.Services.Interfaces;

namespace MyShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IStorageService _storageService;

        public ProductsController(IStorageService storageService)
        {
            _storageService = storageService;
        }


        [HttpGet]
        public async Task<IActionResult> GetProductLists()
        {
            var products = await _storageService.GetAllProducts(); 
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _storageService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

    }
}