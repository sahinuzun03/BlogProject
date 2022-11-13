using KD12BlogProject.Bussiness.Models.DTOs;
using KD12BlogProject.Bussiness.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD12BlogProject.Bussiness.Services.PostService
{
    public interface IPostService
    {
        Task Create(CreatePostDTO model);

        Task Update(UpdatePostDTO model);

        Task Delete(int id);
        Task<UpdatePostDTO> GetById(int id);

        Task<List<PostVM>> GetPosts();

        Task<PostDetailVM> GetPostDetailsVM(int id);

        //Post create işeminde ilk adımda View'a giderken Genre ve Author listesini doldurmak için aşağıdaki fonksiyonu kullanacağız.
        Task<CreatePostDTO> CreatePost();

        Task<List<GetPostsVM>> GetPostsForMembers();
    }
}
