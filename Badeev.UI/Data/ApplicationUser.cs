using Microsoft.AspNetCore.Identity;

namespace Badeev.UI.Data
{
    public class ApplicationUser : IdentityUser
    {
        // Свойство для хранения аватара пользователя (в байтах)
        public byte[]? Avatar { get; set; }
    }
}