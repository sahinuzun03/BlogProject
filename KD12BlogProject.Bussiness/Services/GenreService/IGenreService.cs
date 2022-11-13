using KD12BlogProject.Bussiness.Models.DTOs;
using KD12BlogProject.Bussiness.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD12BlogProject.Bussiness.Services.GenreService
{
    public interface IGenreService
    {
        Task Create(CreateGenreDTO model);
        Task Update(UpdateGenreDTO model);
        Task Delete(int id);

        Task<List<GenreVM>> GetGenres();

        Task<UpdateGenreDTO> GetById(int id);

        Task<bool> isGenreExsist(string Name);
    }
}
