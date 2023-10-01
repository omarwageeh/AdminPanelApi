using Microsoft.AspNetCore.Mvc;
using AmazonClone.Model;
using AmazonClone.Service;
using Microsoft.AspNetCore.Authorization;
using AmazonClone.Dto;
using AmazonClone.Api.Models;

namespace AmazonClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Cors.EnableCors()]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(string? name)
        {
            var products = await _productService.GetProducts(p => !p.IsDeleted && p.NameEn.Contains(name??""));
            var temp = products?.ToList();
            List<ProductDto> result = new List<ProductDto>();
            temp.ForEach(item => result.Add(new ProductDto()
            {
                Id = item.Id,
                NameEn = item.NameEn,
                NameAr = item.NameAr,
                StockQuantity = item.StockQuantity,
                CategoryId = item.CategoryId.ToString(),
                UnitPrice = item.UnitPrice,
                Category = new CategoryDto
                {
                    Id = item.Category.Id,
                    Name = item.Category.Name
                }
            }));
                return Ok(result) ;
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            var product = (await _productService.GetProducts(p=>p.Id == id))!.FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }
            var result = new ProductDto()
            {
                Id = product.Id,
                NameEn = product.NameEn,
                NameAr = product.NameAr,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId.ToString(),
                UnitPrice = product.UnitPrice,
                Category = new CategoryDto()
                {
                    Id = product.Category.Id,
                    Name = product.Category.Name
                }
            };
            return Ok(result);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, ProductUpdateModel product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            await _productService.UpdateProduct(new Product { Id = product.Id, CategoryId=product.CategoryId, NameAr = product.NameAr, NameEn = product.NameEn, StockQuantity = product.StockQuantity, UnitPrice = product.UnitPrice});

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostProduct(Product product)
        {
            await _productService.AddProduct(product);

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = (await _productService.GetProducts(p=>p.Id == id))!.FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            await _productService.DeleteProduct(id);
            return NoContent();
        }

    }
}
