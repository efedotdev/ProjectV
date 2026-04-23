using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]//route attribute ile bu controller a gelen isteklerin "api/products" ile başlamasını sağlıyoruz.
    [ApiController] // Attribute bir classın bir API denetleyicisi olduğunu belirtir. javada Annotation olarak geçer.
    public class ProductsController : ControllerBase
    {
        //Loosely coupled - gevşek bağlılık
        //naming convention
        //javascriptte constractorden de erişim sağlanır.
        //IoC Container -- Inversion of Control
        //configuration = yapıcı metot
        //add scopeped = istek
        //add transient = her seferinde yeni bir nesne oluşturur. 
        //add singleton = uygulama boyunca aynı kalır. 
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        //routing
        [HttpGet("getall")]
        //swagger -- apinin dökümantasyonu
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAllAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add(Product product)
        {
            var result = await _productService.AddAsync(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // WebAPI/Controllers/ProductsController.cs
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] Product product) // [FromBody] eklendi
        {
            var result = await _productService.Delete(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            var result = await _productService.Update(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
