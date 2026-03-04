using Badeev.Domain.Entities;
using Badeev.Domain.Models;

namespace Badeev.UI.Services
{
    public class MemoryCategoryService : ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Автоцистерны", NormalizedName = "fire-engines" },
                new Category { Id = 2, Name = "Автолестницы", NormalizedName = "ladders" },
                new Category { Id = 3, Name = "Штабные авто", NormalizedName = "staff-cars" }
            };

            var result = ResponseData<List<Category>>.OK(categories);
            return Task.FromResult(result);
        }
    }
}