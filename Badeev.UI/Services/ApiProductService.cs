using Badeev.Domain.Entities;
using Badeev.Domain.Models;
using System.Net.Http.Json;

namespace Badeev.UI.Services
{
    public class ApiProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ApiProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseData<List<EquipmentRepair>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var uri = _httpClient.BaseAddress?.AbsoluteUri;
            if (!string.IsNullOrEmpty(categoryNormalizedName))
            {
                uri += $"?category={categoryNormalizedName}";
            }

            var response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<ResponseData<List<EquipmentRepair>>>())!;
            }
            return ResponseData<List<EquipmentRepair>>.Error("Ошибка чтения API техники");
        }
    }
}