namespace Badeev.Domain.Entities
{
    public class EquipmentRepair
    {
        public int Id { get; set; } // уникальный номер

        // Короткое название (например, "АЦ 5.0-50 (МАЗ-530905)")
        public string Name { get; set; } = string.Empty;

        // Описание поломки (например, "Замена вакуумного насоса")
        public string Description { get; set; } = string.Empty;

        // МАТЕМАТИЧЕСКИЙ ПАРАМЕТР: Стоимость ремонта (для подсчета сметы УМЧС)
        public decimal RepairCost { get; set; }

        public string? Image { get; set; } // путь к фото поломки/техники

        // Навигационные свойства (связь с категорией)
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}