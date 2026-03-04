using Badeev.Domain.Entities;
using Badeev.Domain.Models;

namespace Badeev.UI.Services
{
    public class MemoryProductService : IProductService
    {
        private List<EquipmentRepair> _equipments;
        private List<Category> _categories;

        public MemoryProductService(ICategoryService categoryService)
        {
            _categories = categoryService.GetCategoryListAsync().Result.Data!;
            SetupData();
        }

        private void SetupData()
        {
            _equipments = new List<EquipmentRepair>
            {
                new EquipmentRepair { Id = 1, Name = "АЦ 5.0 (МАЗ-5309)", Description = "Замена вакуумного насоса", RepairCost = 1500, Image = "images/1.jpg", CategoryId = _categories.Find(c => c.NormalizedName == "fire-engines")!.Id },
                new EquipmentRepair { Id = 2, Name = "АЦ 8.0 (МАЗ-6302)", Description = "Капитальный ремонт ДВС", RepairCost = 5400, Image = "images/2.jpg", CategoryId = _categories.Find(c => c.NormalizedName == "fire-engines")!.Id },
                new EquipmentRepair { Id = 3, Name = "АЛ-30 (ЗиЛ-131)", Description = "Ремонт гидравлики стрелы", RepairCost = 2100, Image = "images/3.jpg", CategoryId = _categories.Find(c => c.NormalizedName == "ladders")!.Id },
                new EquipmentRepair { Id = 4, Name = "Geely Atlas (Штабной)", Description = "Плановое ТО-4", RepairCost = 450, Image = "images/4.jpg", CategoryId = _categories.Find(c => c.NormalizedName == "staff-cars")!.Id }
            };
        }

        public Task<ResponseData<List<EquipmentRepair>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            // Получаем ID категории, если она передана
            int? categoryId = null;
            if (categoryNormalizedName != null)
            {
                categoryId = _categories.Find(c => c.NormalizedName == categoryNormalizedName)?.Id;
            }

            // Фильтруем
            var data = _equipments
                .Where(d => categoryId == null || d.CategoryId == categoryId)
                .ToList();

            var result = data.Count == 0
                ? ResponseData<List<EquipmentRepair>>.Error("Нет техники в данной категории")
                : ResponseData<List<EquipmentRepair>>.OK(data);

            return Task.FromResult(result);
        }
    }
}