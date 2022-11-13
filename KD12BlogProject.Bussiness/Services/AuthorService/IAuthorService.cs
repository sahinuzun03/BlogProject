using KD12BlogProject.Bussiness.Models.DTOs;
using KD12BlogProject.Bussiness.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD12BlogProject.Bussiness.Services.AuthorService
{
    public interface IAuthorService
    {
        Task Create(CreateAuthorDTO model);
        Task Update(UpdateAuthorDTO model);
        Task Delete(int id);

        Task<List<AuthorVM>> GetAuthors();

        Task<AuthorDetailVM> GetDetails(int id);

        Task<UpdateAuthorDTO> GetById(int id);

        Task<bool> isAuthorExsist(string firstName, string lastName);
    }
}
