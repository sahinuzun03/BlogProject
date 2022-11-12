using KD12BlogProject.Bussiness.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD12BlogProject.Bussiness.Services.AppUserService
{
    public class AppUserService : IAppUserService
    {
        //Burada Constructor çalıştırıp gerekli başlatmaları yapacağız
        public Task<UpdateProfileDTO> GetByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<SignInResult> Login(LoginDTO model)
        {
            throw new NotImplementedException();
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> Register(RegisterDTO model)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(UpdateProfileDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
