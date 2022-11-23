using KD12BlogProject.Bussiness.Models.DTOs;
using KD12BlogProject.Bussiness.Services.AppUserService;
using KD12BlogProject.Bussiness.Services.AuthorService;
using Microsoft.AspNetCore.Mvc;

namespace KD12BlogProject.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authorService.isAuthorExsist(model.FirstName, model.LastName);

                if (!result)
                {
                    await _authorService.Create(model);
                    TempData["Success"] = $"{model.FirstName} {model.LastName} has been added..!";
                    return RedirectToAction("List");
                }
                else
                {
                    TempData["Error"] = $"Author already exist..!";
                    return View(model);
                }
            }
            else
            {
                TempData["Error"] = $"Author hasn't been added..!";
                return View(model);
            }

        }

        public async Task<IActionResult> List()
        {
            var result = await _authorService.GetAuthors();
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var author = await _authorService.GetDetails(id);

            return View(author);
        }

        public async Task<IActionResult> Update(int id)
        {
            var model = await _authorService.GetById(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateAuthorDTO model)
        {
            if (ModelState.IsValid)
            {
                //is author alredy exist kontrolü yapabilirsiniz

                await _authorService.Update(model);
                TempData["Success"] = "Author has been updated..!";
                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "Author hasn't been updated..!";
                return View(model);
            }
        }
    }
}
