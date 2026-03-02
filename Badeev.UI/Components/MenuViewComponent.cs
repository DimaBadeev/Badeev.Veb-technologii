using Microsoft.AspNetCore.Mvc;

namespace Badeev.UI.Components
{
    public class MenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // Получаем имя контроллера и области для подсветки активного пункта меню
            ViewData["Controller"] = Request.RouteValues["controller"]?.ToString()?.ToLower() ?? string.Empty;
            ViewData["Area"] = Request.RouteValues["area"]?.ToString()?.ToLower() ?? string.Empty;

            return View();
        }
    }
}