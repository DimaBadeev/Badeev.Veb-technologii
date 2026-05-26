using Badeev.Domain.Entities;
using Badeev.Domain.Models;
using System.Net.Http.Json;

namespace Badeev.UI.Services
{
    public class ApiCategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public ApiCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<ResponseData<List<Category>>>())!;
            }
            return ResponseData<List<Category>>.Error("Ошибка чтения API категорий");
        }
    }
}