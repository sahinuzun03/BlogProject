using KD12BlogProject.Bussiness.Models.DTOs;
using KD12BlogProject.Bussiness.Services.GenreService;
using Microsoft.AspNetCore.Mvc;

namespace KD12BlogProject.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenreController : Controller
	{
		private readonly IGenreService _genreService;

		public GenreController(IGenreService genreService)
		{
			_genreService = genreService;
		}

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGenreDTO model)
        {
            if (ModelState.IsValid)
            {
                //Var olan genre'yı bir daha eklememek için önlem aldık. Model'den bize gelen Name veri tabanında varsa result true yoksa false dönecektir. result'tın durumana göre ekleme işlemini yapacağız.
                bool result = await _genreService.isGenreExsist(model.Name);

                if (!result)
                {
                    TempData["Success"] = $"{model.Name} has been added..!";
                    await _genreService.Create(model);
                    return RedirectToAction("List");
                }
                else
                {
                    TempData["Warning"] = $"{model.Name} already has been exist..!";
                    return View(model);
                }
            }
            else
            {
                TempData["Error"] = $"{model.Name} hasn't been added..!";
                return View(model);
            }

        }

        public async Task<IActionResult> List()
        {
            return View(await _genreService.GetGenres());
        }

        public async Task<IActionResult> Update(int id)
        {
            var genre = await _genreService.GetById(id);
            return View(genre);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateGenreDTO model)
        {
            if (ModelState.IsValid)
            {
                await _genreService.Update(model);
                return RedirectToAction("List");
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _genreService.Delete(id);
            return RedirectToAction("List");
        }
    }
}
