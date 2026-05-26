using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Badeev.Domain.Entities;
using Badeev.Domain.Models;

namespace Badeev.Blazor.Services
{
    public class ApiProductService : IProductService<EquipmentRepair>
    {
        private readonly HttpClient _http;
        private List<EquipmentRepair> _equipments = new();
        private int _currentPage = 1;
        private int _totalPages = 1;

        public ApiProductService(HttpClient http)
        {
            _http = http;
        }

        public IEnumerable<EquipmentRepair> Products => _equipments;
        public int CurrentPage => _currentPage;
        public int TotalPages => _totalPages;

        public event Action? ListChanged;

        public async Task GetProducts(int pageNo = 1, int pageSize = 3)
        {
            // Отправляем запрос к нашему API
            var response = await _http.GetAsync(_http.BaseAddress);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<ResponseData<List<EquipmentRepair>>>();

                if (responseData != null && responseData.Success && responseData.Data != null)
                {
                    _currentPage = pageNo;
                    _totalPages = (int)Math.Ceiling(responseData.Data.Count / (double)pageSize);

                    // Делаем постраничный вывод (пагинацию) на стороне Blazor
                    _equipments = responseData.Data
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

                    ListChanged?.Invoke();
                }
            }
            else
            {
                _equipments = new List<EquipmentRepair>();
                _currentPage = 1;
                _totalPages = 0;
                ListChanged?.Invoke();
            }
        }
    }
}