using Badeev.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Добавляем поддержку контроллеров REST API
builder.Services.AddControllers();

// Подключаем SQLite базу данных для API
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

var app = builder.Build();

app.UseHttpsRedirection();

// РАЗРЕШАЕМ РАЗДАЧУ КАРТИНОК ИЗ wwwroot 
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

// Автозапуск наполнения БД при старте API
await DbInitializer.SeedData(app);

app.Run();