using Badeev.API.Data;
using Badeev.Domain.Entities;
using Badeev.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Badeev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<IEnumerable<Category>>>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(ResponseData<IEnumerable<Category>>.OK(categories));
        }
    }
}