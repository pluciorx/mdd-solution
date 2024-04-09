using mddPostFetcher.Models;

namespace mddPostFetcher.Interfaces
{
    public interface IPostFetcher
    {
        Task<IEnumerable<CommentModel>> FetchCommentsAsync();
        Task<IEnumerable<PostModel>> FetchPostsAsync();
        Task<ICollection<PostModel>> GetPostWithCommentsAsync();
        Task<ICollection<PostModel>> GetPostWithCommentsLinqAsync();
    }
}
