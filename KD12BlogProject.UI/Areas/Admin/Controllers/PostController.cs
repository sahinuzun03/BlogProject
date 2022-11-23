using KD12BlogProject.Bussiness.Models.DTOs;
using KD12BlogProject.Bussiness.Services.PostService;
using Microsoft.AspNetCore.Mvc;

namespace KD12BlogProject.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
	{
		private readonly IPostService _postService;
		public PostController(IPostService postService)
		{
			_postService = postService;
		}
        public async Task<IActionResult> Create() => View(await _postService.CreatePost());

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostDTO model)
        {
            if (ModelState.IsValid)
            {
                await _postService.Create(model);
                TempData["Success"] = "Post has been added..!";
                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "Post hasn't been added..!";
                return View(model);
            }

        }

        public async Task<IActionResult> List() => View(await _postService.GetPosts());

        public async Task<IActionResult> Details(int id) => View(await _postService.GetPostDetailsVM(id));

        public async Task<IActionResult> Update(int id) => View(await _postService.GetById(id));


        [HttpPost]
        public async Task<IActionResult> Update(UpdatePostDTO model)
        {
            if (ModelState.IsValid)
            {
                await _postService.Update(model);
                TempData["Success"] = "Post has been updated..!";
                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "Post hasn't been updated..!";
                return View(model);
            }
        }
    }
}
