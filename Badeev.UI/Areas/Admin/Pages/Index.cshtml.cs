using Badeev.Domain.Entities;
using Badeev.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Badeev.UI.Areas.Admin.Pages
{
    [Authorize(Policy = "admin")]
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public List<EquipmentRepair> Equipments { get; set; } = new();

        public async Task OnGetAsync()
        {
            var response = await _productService.GetProductListAsync(null);
            if (response.Success && response.Data != null)
            {
                Equipments = response.Data;
            }
        }
    }
}