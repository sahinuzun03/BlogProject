using KD12BlogProject.Bussiness.Services.PostService;
using Microsoft.AspNetCore.Mvc;

namespace KD12BlogProject.UI.Areas.Member.Controllers
{
    [Area("Member")]
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        public HomeController(IPostService postService) => _postService = postService;
        public async Task<IActionResult> Index()
        {
            return View(await _postService.GetPostsForMembers());
        }
    }
}
