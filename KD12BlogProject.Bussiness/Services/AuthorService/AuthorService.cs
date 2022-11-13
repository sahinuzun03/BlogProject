using AutoMapper;
using KD12BlogProject.Bussiness.Models.DTOs;
using KD12BlogProject.Bussiness.Models.VMs;
using KD12BlogProject.Core.Enums;
using KD12BlogProject.DataAccess.Abstract;
using KD12BlogProject.Entities.Concrete;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD12BlogProject.Bussiness.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepo;
        private readonly IMapper _mapper;

        public AuthorService(IMapper mapper,
                             IAuthorRepository authorRepo)
        {
            _authorRepo = authorRepo;
            _mapper = mapper;
        }

        public async Task Create(CreateAuthorDTO model)
        {
            var author = _mapper.Map<Author>(model);

            if (author.UploadPath != null)//şayet kullanıcı resim eklemişse
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream()); //kullanıcı tarafından eklenen dosyanın yolunu okuduk.
                image.Mutate(x => x.Resize(600, 560)); //yukarıda elde ettiğimiz resmi resize ettik.
                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg"); //yazar resimlerinin karışmaması için her bir resme biricik gui atatdık. Ayrıca iligi dosya yolunada aynı resmi ekledik.
                author.ImagePath = ($"/images/{guid}.jpg");//Authors tablosuna eklenecek olan author nesnesinin ImagePath property'sine resim yolumuzu eklediki
                await _authorRepo.Create(author);
            }
            else
            {
                author.ImagePath = ($"/images/default.jpg");//kullanıcı resim eklememişse belirtilen ilgili yoldaki default resim eklenecek.
                await _authorRepo.Create(author);
            }
        }

        public async Task Delete(int id)
        {
            var author = await _authorRepo.GetDefault(x => x.Id == id);
            author.DeletedDate = DateTime.Now;
            author.Status = Status.Passive;
            await _authorRepo.Delete(author);
        }

        public async Task<List<AuthorVM>> GetAuthors()
        {
            var authors = await _authorRepo.GetFilteredList(
                select: x => new AuthorVM
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ImagePath = x.ImagePath,
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.FirstName));

            return authors;
        }

        public async Task<UpdateAuthorDTO> GetById(int id)
        {
            var author = await _authorRepo.GetFilteredFirstOrDefault(
                select: x => new AuthorVM
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ImagePath = x.ImagePath,
                },
                where: x => x.Id == id);

            var model = _mapper.Map<UpdateAuthorDTO>(author);

            return model;
        }

        public async Task<AuthorDetailVM> GetDetails(int id)
        {
            var author = await _authorRepo.GetFilteredFirstOrDefault(
                select: x => new AuthorDetailVM
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ImagePath = x.ImagePath,
                    CreateDate = x.CreatedDate
                },
                where: x => x.Id == id);

            return author;
        }

        public async Task<bool> isAuthorExsist(string firstName, string lastName)
        {
            var result = await _authorRepo.Any(x => x.FirstName == firstName && x.LastName == lastName);
            return result;
        }

        public async Task Update(UpdateAuthorDTO model)
        {
            var author = _mapper.Map<Author>(model);

            if (author.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(600, 560));
                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");
                author.ImagePath = ($"/images/{guid}.jpg");

                await _authorRepo.Update(author);
            }
            else
            {
                author.ImagePath = model.ImagePath;
                await _authorRepo.Update(author);
            }
        }
    }
}
