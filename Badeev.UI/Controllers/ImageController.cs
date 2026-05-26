using Badeev.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Badeev.UI.Controllers
{
    public class ImageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public ImageController(UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _env = env;
        }

        public async Task<IActionResult> GetAvatar()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user?.Avatar != null)
            {
                return File(user.Avatar, "image/png");
            }

            // Если аватарки в базе нет, отдаем дефолтную картинку заглушку
            var defaultPath = Path.Combine(_env.WebRootPath, "images", "default-profile-picture.png");
            return File(defaultPath, "image/png");
        }
    }
}