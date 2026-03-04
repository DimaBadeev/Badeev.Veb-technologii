using Badeev.Domain.Entities;
using Badeev.Domain.Models;

namespace Badeev.UI.Services
{
    public interface ICategoryService
    {
        Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}