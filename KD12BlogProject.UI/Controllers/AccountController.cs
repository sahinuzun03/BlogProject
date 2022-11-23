using KD12BlogProject.Bussiness.Models.DTOs;
using KD12BlogProject.Bussiness.Services.AppUserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KD12BlogProject.UI.Controllers
{
    [Authorize]//Bu attribute ile sisteme login olunma zorunluluğu getiriyoruz. Sistem sağlıklı bir şeklde çalıştığında bütün controller'ları bu attribute ile işaretleyeceğiz. Örneğin bir e ticaret sitesinde kullanıcılara login olmadan ürünleri görebilmektedir. O halde ürünleri gösteren controller bu attribu ile işaretlenmemiştir. Sepet ile ilgili işlem yapıldığında ise günlük hayattan hatırlayacağınız gibi uygulamalar bizi login olmaya zorlamaktadır. Bu bağlamda sepet controller'ı "Authorize" attribute ile işaretlenmiştir. Bizim uygulamamızda admin area bulunan bütün controller'lar bu attribu ile işaretlenecektir. Çünkü kimlik doğrulaması yaparak sisteme girmiş kişiler ilgii işlemeleri yapabilme yeteneğine sahip olacaktır.
    [AutoValidateAntiforgeryToken] //Uygulamaya Register yada Login olma isteği atan kullanıcının güvenlik gereği işlem yaparken bir token üretmesi bu token'ın meta bilgileri yani üst bilgileir ile "who am i" sorusuna cevap vermesi yani güvenlik önemlerini geçmesi için bir token üretilir bu token request sonucunda çözünür. Böylelikle uçtan uca güvenli bir şekilde talepler yerine getirilir. Aslında bu işlemi diğer bütün işlemlerimiz için yapabilirz. Post, Genre vb CRUD işlemlerinde. 
    public class AccountController : Controller
    {
        private readonly IAppUserService _appUserService;

        public AccountController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated) //eğer kullanıcı hali hazıda sisteme authenticate olmuşsa
            {
                return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
            }
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _appUserService.Register(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
                }

                //Indentity'Nin içerisinde gömülü olarak bulunan Errors listesinin içerisinde dolaştım. 31. satırda result error ile dolarsa hani hata olduğunu spesifik olarka yazdırdım.
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                    TempData["Error"] = "Something went wrong..!";
                }
            }
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult LogIn(string returnUrl = "/")
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
            }
            ViewData["ReturnUrl"] = returnUrl; //hidden input'ta alternatif
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> LogIn(LoginDTO model, string returnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                var result = await _appUserService.Login(model);

                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }

                ModelState.AddModelError("", "Invalid Login Attapt..!");
            }
            return View(model);
        }
        private IActionResult RedirectToLocal(string returnUrl = "/")
        {
            //IsLocalUrl() fonksiyonu, parametresine aldığı değerin yerel bir URL olup olmadığını kontrol eder. Bir URL bizim domain alanımızda ise yani bizim yetki alanımızda ise bize true değilse false dönecektir.
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
            }
        }

        public async Task<IActionResult> Edit(string userName)
        {
            var user = await _appUserService.GetByUserName(userName);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProfileDTO model)
        {
            if (ModelState.IsValid)
            {
                await _appUserService.UpdateUser(model);
                TempData["Success"] = "Your profile has been updated..!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                TempData["Error"] = "Your profile hasn't been updated..!";
                return View(model);
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
