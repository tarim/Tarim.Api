using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Tarim.Api.Infrastructure.Interface;
using Tarim.Api.Infrastructure.Model.Products;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tarim.Api.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        // GET: api/values
        [HttpGet("{pageNumber:int}")]
        public async Task<IActionResult> GetAsync(int pageNumber)
        {
            var result = await _productsRepository.GetProducts(pageNumber);
            return Ok(result.Object);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productsRepository.GetAllProducts();
            return Ok(result.Object);
        }


        [HttpGet("product/{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await _productsRepository.GetProduct(id);
            return Ok(result.Object);
        }

        // POST api/values
       // [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                int userRecid = 0;
                var result = await _productsRepository.AddProduct(product,userRecid);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }

        // PUT api/values/5
        [HttpPut("product/{id:int}")]
        public async Task<IActionResult> PutAsync([FromBody] Product product, int id)
        {
            if (ModelState.IsValid && product.Id > 0 && product.Id == id)
            {
                var result = await _productsRepository.UpdateProduct(product);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _productsRepository.DeleteProduct(id);
            return Ok(result);
        }

    }
}
