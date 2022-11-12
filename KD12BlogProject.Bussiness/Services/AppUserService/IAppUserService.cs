using KD12BlogProject.Bussiness.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD12BlogProject.Bussiness.Services.AppUserService
{
    public interface IAppUserService
    {
        Task<IdentityResult> Register(RegisterDTO model);
        Task<SignInResult> Login(LoginDTO model);

        Task Logout();

        Task UpdateUser(UpdateProfileDTO model);

        Task<UpdateProfileDTO> GetByUserName(string userName);
    }
}
