using AutoMapper;
using KD12BlogProject.Bussiness.Models.DTOs;
using KD12BlogProject.Bussiness.Models.VMs;
using KD12BlogProject.Core.Enums;
using KD12BlogProject.DataAccess.Abstract;
using KD12BlogProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD12BlogProject.Bussiness.Services.GenreService
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepo;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepo,
                            IMapper mapper)
        {
            _genreRepo = genreRepo;
            _mapper = mapper;
        }

        public async Task Create(CreateGenreDTO model)
        {
            var genre = _mapper.Map<Genre>(model);

            await _genreRepo.Create(genre);
        }

        public async Task Delete(int id)
        {
            Genre genre = await _genreRepo.GetDefault(x => x.Id == id);
            genre.DeletedDate = DateTime.Now;
            genre.Status = Status.Passive;

            await _genreRepo.Delete(genre);
        }

        public async Task<UpdateGenreDTO> GetById(int id)
        {
            var genre = await _genreRepo.GetFilteredFirstOrDefault(
                select: x => new GenreVM
                {
                    Id = x.Id,
                    Name = x.Name,
                },
                where: x => x.Id == id);

            var model = _mapper.Map<UpdateGenreDTO>(genre);

            return model;
        }

        public Task<List<GenreVM>> GetGenres()
        {
            var genres = _genreRepo.GetFilteredList(
                select: x => new GenreVM
                {
                    Id = x.Id,
                    Name = x.Name,
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Name));

            return genres;
        }

        public Task<bool> isGenreExsist(string Name)
        {
            var result = _genreRepo.Any(x => x.Name == Name);
            return result;
        }

        public async Task Update(UpdateGenreDTO model)
        {
            var updatedGenre = _mapper.Map<Genre>(model);

            await _genreRepo.Update(updatedGenre);
        }
    }
}
