using Badeev.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Badeev.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<EquipmentRepair> EquipmentRepairs { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}