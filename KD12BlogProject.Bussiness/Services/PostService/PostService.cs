using AutoMapper;
using KD12BlogProject.Bussiness.Models.DTOs;
using KD12BlogProject.Bussiness.Models.VMs;
using KD12BlogProject.Core.Enums;
using KD12BlogProject.DataAccess.Abstract;
using KD12BlogProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD12BlogProject.Bussiness.Services.PostService
{
    public class PostService
    {
        private readonly IGenreRepository _genreRepo;
        private readonly IAuthorRepository _authorRepo;
        private readonly IPostRepository _postRepo;
        private readonly IMapper _mapper;

        public PostService(IGenreRepository genreRepo,
                           IAuthorRepository authorRepo,
                           IPostRepository postRepo,
                           IMapper mapper)
        {
            _genreRepo = genreRepo;
            _authorRepo = authorRepo;
            _postRepo = postRepo;
            _mapper = mapper;
        }

        public async Task Create(CreatePostDTO model)
        {
            var post = _mapper.Map<Post>(model);

            if (post.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(600, 560));
                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");
                post.ImagePath = ($"/images/{guid}.jpg");

                await _postRepo.Create(post);
            }
            else
            {
                post.ImagePath = $"/images/defaultpost.jpg";
                await _postRepo.Create(post);
            }
        }

        //Create işleminde get operasyonunda kullanıclacaktır
        public async Task<CreatePostDTO> CreatePost()
        {
            CreatePostDTO model = new CreatePostDTO()
            {
                Genres = await _genreRepo.GetFilteredList(
                    select: x => new GenreVM
                    {
                        Id = x.Id,
                        Name = x.Name,
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.Name)),

                Authors = await _authorRepo.GetFilteredList(
                    select: x => new AuthorVM
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.FirstName))
            };

            return model;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdatePostDTO> GetById(int id)
        {
            var post = await _postRepo.GetFilteredFirstOrDefault(
                select: x => new PostVM
                {
                    Title = x.Title,
                    Content = x.Content,
                    ImagePath = x.ImagePath,
                    GenreId = x.GenreId,
                    AuthorId = x.AuthorId
                },
                where: x => x.Id == id);

            var model = _mapper.Map<UpdatePostDTO>(post);

            model.Authors = await _authorRepo.GetFilteredList(
                select: x => new AuthorVM
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.FirstName));

            model.Genres = await _genreRepo.GetFilteredList(
                select: x => new GenreVM
                {
                    Id = x.Id,
                    Name = x.Name,
                },
            where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Name));

            return model;
        }

        public async Task<PostDetailVM> GetPostDetailsVM(int id)
        {
            var post = await _postRepo.GetFilteredFirstOrDefault(
                select: x => new PostDetailVM
                {
                    Title = x.Title,
                    Content = x.Content,
                    ImagePath = x.ImagePath,
                    CreateDate = x.CreatedDate,
                    AuthorImagePath = x.Author.ImagePath,
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName,
                    //Comments = x.Comments.Where(x=> x.PostId == id).
                    //.Orderbydescending(x=> x.CreateDate)
                    //.Select(x=> new CommentVM
                    //{
                    //    Text = x.Text,
                    //    UserImage = x.AppUser.ImagePath,
                    //    UserName = x.AppUser.UserName,
                    //    Create = x.CreateDate
                    //
                    //}).ToListAsync();
                },
                where: x => x.Id == id,
                orderBy: x => x.OrderBy(x => x.Title),
                include: x => x.Include(x => x.Author)); //.Include(x=> x.Comments); //bunu neden yaptık çünkü yukarıda yorum tablosunuda sorgumuza kattık.

            return post;
        }

        public async Task<List<PostVM>> GetPosts()
        {
            var posts = await _postRepo.GetFilteredList(
                select: x => new PostVM
                {
                    Id = x.Id,
                    Title = x.Title,
                    GenreName = x.Genre.Name,
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName,
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Title),
                include: x => x.Include(x => x.Genre)
                              .Include(x => x.Author));

            return posts;
        }

        public async Task<List<GetPostsVM>> GetPostsForMembers()
        {
            var posts = await _postRepo.GetFilteredList(
                select: x => new GetPostsVM
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    ImagePath = x.ImagePath,
                    CreateDate = x.CreatedDate,
                    UserImagePath = x.Author.ImagePath,
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName,
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderByDescending(x => x.CreatedDate),
                include: x => x.Include(x => x.Author));

            return posts;
        }

        public async Task Update(UpdatePostDTO model)
        {
            var post = _mapper.Map<Post>(model);

            if (post.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(600, 560));
                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");
                post.ImagePath = ($"/images/{guid}.jpg");

                await _postRepo.Update(post);
            }
            else
            {
                post.ImagePath = model.ImagePath;
                await _postRepo.Update(post);
            }
        }
    }
}
