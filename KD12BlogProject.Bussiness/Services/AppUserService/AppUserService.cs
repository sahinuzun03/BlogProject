using AutoMapper;
using KD12BlogProject.Bussiness.Models.DTOs;
using KD12BlogProject.DataAccess.Abstract;
using KD12BlogProject.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD12BlogProject.Bussiness.Services.AppUserService
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper; //Mapper kütüphanesini kullanacağız o yüdn çağırdık
        private readonly UserManager<AppUser> _userManager; //A-Z ' ye kullanıcı yönetimini sağladığımız yapı
        private readonly SignInManager<AppUser> _singInManager; //Kullanıcı giriş ve çıkışlarını kontrol eden sınıfımız!!

        public AppUserService(IAppUserRepository appUserRepository, IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> singInManager)
        {
            _appUserRepository = appUserRepository;
            _mapper = mapper;
            _userManager = userManager;
            _singInManager = singInManager;
        }

        public async Task<UpdateProfileDTO> GetByUserName(string userName)
        {
            var user = await _appUserRepository.GetFilteredFirstOrDefault(select: x => new UpdateProfileDTO
            {
                Id = x.Id,
                UserName = x.UserName,
                Password = x.PasswordHash,
                Email = x.Email,
                ImagePath = x.ImagePath
            }, where: x => x.UserName == userName);

            return user;
        }

        public async Task<SignInResult> Login(LoginDTO model)
        {
            var result = await _singInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            return result;
        }

        public async Task Logout()
        {
            await _singInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterDTO model)
        {
            var user = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _singInManager.SignInAsync(user, isPersistent: false);
            }
            return result;
        }

        public async Task UpdateUser(UpdateProfileDTO model)
        {
            var user = await _appUserRepository.GetDefault(x => x.Id == model.Id);

            if (model.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(600, 560));
                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");
                user.ImagePath = ($"/images/{guid}.jpg");

                await _appUserRepository.Update(user);
            }

            if (model.Password != null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password); //string olarak gelen password'ü hash'leyecek
                await _userManager.UpdateAsync(user); //identity'nin UpdateAsync methodunu kullanıyor
            }

            if (model.UserName != null)
            {
                var isUserNameExist = await _userManager.FindByNameAsync(model.UserName);

                if (isUserNameExist == null)
                {
                    await _userManager.SetUserNameAsync(user, model.UserName);
                    await _singInManager.SignInAsync(user, isPersistent: false);
                }
            }

            if (model.Email != null)
            {
                var isUserEmailExist = await _userManager.FindByEmailAsync(model.Email);

                if (isUserEmailExist == null)
                {
                    await _userManager.SetEmailAsync(user, model.Email);
                }
            }
        }
    }
}
