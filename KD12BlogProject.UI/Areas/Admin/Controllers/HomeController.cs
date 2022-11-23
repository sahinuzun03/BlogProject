using Microsoft.AspNetCore.Mvc;

namespace KD12BlogProject.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
