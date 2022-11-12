using AutoMapper;
using KD12BlogProject.Bussiness.Models.DTOs;
using KD12BlogProject.Bussiness.Models.VMs;
using KD12BlogProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD12BlogProject.Bussiness.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Genre, CreateGenreDTO>().ReverseMap();
            CreateMap<Genre, UpdateGenreDTO>().ReverseMap();
            CreateMap<Genre, GenreVM>().ReverseMap();
            CreateMap<UpdateGenreDTO, GenreVM>().ReverseMap();

            CreateMap<Author, CreateAuthorDTO>().ReverseMap();
            CreateMap<Author, UpdateAuthorDTO>().ReverseMap();
            CreateMap<Author, AuthorVM>().ReverseMap();
            CreateMap<UpdateAuthorDTO, AuthorVM>().ReverseMap();

            CreateMap<Post, CreatePostDTO>().ReverseMap();
            CreateMap<Post, UpdatePostDTO>().ReverseMap();
            CreateMap<Post, PostVM>().ReverseMap();
            CreateMap<UpdatePostDTO, PostVM>().ReverseMap();


            CreateMap<AppUser, RegisterDTO>().ReverseMap();
            CreateMap<AppUser, LoginDTO>().ReverseMap();
            CreateMap<AppUser, UpdateProfileDTO>().ReverseMap();
        }
    }
}
