using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; // Важно для работы MigrateAsync!
using System.Security.Claims;

namespace Badeev.UI.Data
{
    // Сделали класс статическим (исправляет предупреждения S1118 и RCS1102)
    public static class DbInit
    {
        public static async Task SetupIdentityAdmin(WebApplication application)
        {
            using var scope = application.Services.CreateScope();

            // 1. Получаем контекст базы данных
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await context.Database.MigrateAsync();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var adminEmail = "admin@mchs.gov.by";
            var user = await userManager.FindByEmailAsync(adminEmail);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "123456");
                if (result.Succeeded)
                {
                    // Добавляем клейм роли "admin"
                    var claim = new Claim(ClaimTypes.Role, "admin");
                    await userManager.AddClaimAsync(user, claim);
                }
            }
        }
    }
}