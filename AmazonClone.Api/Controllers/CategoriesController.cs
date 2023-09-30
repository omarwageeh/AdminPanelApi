using Microsoft.AspNetCore.Mvc;
using AmazonClone.Model;
using AmazonClone.Service;
using Microsoft.AspNetCore.Authorization;

namespace AmazonClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories(string? name)
        {
            var categories = await _categoryService.GetCategories(c=>c.Name.Contains(name??""));
            return categories?.ToList()!;
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
        
            var category = await _categoryService.GetCategories(p=>p.Id == id);

            if (category?.FirstOrDefault() == null)
            {
                return NotFound();
            }

            return category.FirstOrDefault()!;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(Guid id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            await _categoryService.UpdateCategory(category);

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            await _categoryService.AddCategory(category);

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _categoryService.GetCategories(c=>c.Id == id);
            if (category?.FirstOrDefault() == null)
            {
                return NotFound();
            }

            await _categoryService.DeleteCategory(category.FirstOrDefault()!);

            return NoContent();
        }
    }
}
