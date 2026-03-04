using Badeev.Domain.Entities;
using Badeev.Domain.Models;

namespace Badeev.UI.Services
{
    public interface IProductService
    {
        Task<ResponseData<List<EquipmentRepair>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1);
    }
}