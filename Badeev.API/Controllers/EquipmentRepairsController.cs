using Badeev.API.Data;
using Badeev.Domain.Entities;
using Badeev.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Badeev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentRepairsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EquipmentRepairsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<List<EquipmentRepair>>>> GetEquipmentRepairs(string? category)
        {
            var query = _context.EquipmentRepairs.Include(e => e.Category).AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(e => e.Category!.NormalizedName == category);
            }

            var data = await query.ToListAsync();

            if (data.Count == 0)
            {
                return Ok(ResponseData<List<EquipmentRepair>>.Error("Нет техники в данной категории"));
            }

            return Ok(ResponseData<List<EquipmentRepair>>.OK(data));
        }

        // Сохранение картинок во вспомогательном API-методе
        [HttpPost("{id}")]
        public async Task<IActionResult> SaveImage(int id, IFormFile image)
        {
            var equipment = await _context.EquipmentRepairs.FindAsync(id);
            if (equipment == null) return NotFound();

            var imagesPath = Path.Combine(_env.WebRootPath, "images");
            if (!Directory.Exists(imagesPath)) Directory.CreateDirectory(imagesPath);

            var randomName = Path.GetRandomFileName();
            var extension = Path.GetExtension(image.FileName);
            var fileName = Path.ChangeExtension(randomName, extension);
            var filePath = Path.Combine(imagesPath, fileName);

            using (var stream = System.IO.File.OpenWrite(filePath))
            {
                await image.CopyToAsync(stream);
            }

            var host = "https://" + Request.Host;
            equipment.Image = $"{host}/images/{fileName}";
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}