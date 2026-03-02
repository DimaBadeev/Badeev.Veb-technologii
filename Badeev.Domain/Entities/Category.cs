namespace Badeev.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        // Название категории (например, "Пожарные автоцистерны", "Штабные авто")
        public string Name { get; set; } = string.Empty;

        // Для URL (например, "fire-engines")
        public string NormalizedName { get; set; } = string.Empty;
    }
}