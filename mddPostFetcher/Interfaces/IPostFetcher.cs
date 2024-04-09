using mddPostFetcher.Models;

namespace mddPostFetcher.Interfaces
{
    public interface IPostFetcher
    {      
        Task<ICollection<PostModel>> GetPostWithCommentsAsync();
        Task<ICollection<PostModel>> GetPostWithCommentsLinqAsync();
    }
}
