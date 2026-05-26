using Badeev.API.Data;
using Badeev.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Badeev.API.Data
{
    public static class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            var uri = "https://localhost:7002/"; // Адрес API

            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await context.Database.MigrateAsync();

            if (!context.Categories.Any() && !context.EquipmentRepairs.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Автоцистерны", NormalizedName = "fire-engines" },
                    new Category { Name = "Автолестницы", NormalizedName = "ladders" },
                    new Category { Name = "Штабные авто", NormalizedName = "staff-cars" }
                };

                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();

                var equipments = new List<EquipmentRepair>
                {
                    new EquipmentRepair { Name = "АЦ 5.0 (МАЗ-5309)", Description = "Замена вакуумного насоса", RepairCost = 1500, Image = uri + "images/1.jpg", CategoryId = categories[0].Id },
                    new EquipmentRepair { Name = "АЦ 8.0 (МАЗ-6302)", Description = "Капитальный ремонт ДВС", RepairCost = 5400, Image = uri + "images/2.jpg", CategoryId = categories[0].Id },
                    new EquipmentRepair { Name = "АЛ-30 (ЗиЛ-131)", Description = "Ремонт гидравлики стрелы", RepairCost = 2100, Image = uri + "images/3.jpg", CategoryId = categories[1].Id },
                    new EquipmentRepair { Name = "Geely Atlas (Штабной)", Description = "Плановое ТО-4", RepairCost = 450, Image = uri + "images/4.jpg", CategoryId = categories[2].Id }
                };

                await context.EquipmentRepairs.AddRangeAsync(equipments);
                await context.SaveChangesAsync();
            }
        }
    }
}