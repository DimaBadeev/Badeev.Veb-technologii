using Badeev.Blazor.Components;
using Badeev.Blazor.Services;
using Badeev.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// РЕГИСТРИРУЕМ СТАНДАРТНЫЙ HTTP-КЛИЕНТ 
builder.Services.AddHttpClient();

// Регистрация клиента
builder.Services.AddHttpClient<IProductService<EquipmentRepair>, ApiProductService>(c =>
    c.BaseAddress = new Uri("https://localhost:7002/api/equipmentrepairs"))
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
    });

var app = builder.Build();

// Настройка конвейера запросов HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// 2. ОТОБРАЖЕНИЕ КОМПОНЕНТОВ 
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();