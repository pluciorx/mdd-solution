using mddPostFetcher.Interfaces;
using mddPostFetcher.Models;
using System.Diagnostics;
using System.Net.Http.Json;

namespace mddPostFetcher
{
    public class URLPostDataFetcher : IPostFetcher
    {
        private Uri _postUri;
        private Uri _commentUri;
        private IEnumerable<PostModel> _posts;
        private IEnumerable<CommentModel> _comments;

        static readonly HttpClient client = new HttpClient();
        public URLPostDataFetcher(string commentURL, string postURL)
        {
            if (!Uri.TryCreate(commentURL, UriKind.Absolute, out _commentUri)
                && (_commentUri.Scheme == Uri.UriSchemeHttp || _commentUri.Scheme == Uri.UriSchemeHttps)) throw new ArgumentException("Invalid Comment URL provided");

            if (!Uri.TryCreate(postURL, UriKind.Absolute, out _postUri)
                && (_postUri.Scheme == Uri.UriSchemeHttp || _postUri.Scheme == Uri.UriSchemeHttps)) throw new ArgumentException("Invalid Post URL provided");

        }

        private async Task<IEnumerable<CommentModel>> FetchCommentsAsync()
        {
            if (_commentUri == null) throw new ArgumentException("Comment Uri not constructed");
            _comments = await client.GetFromJsonAsync<List<CommentModel>>(_commentUri);
            return _comments;
        }

        private async Task<IEnumerable<PostModel>> FetchPostsAsync()
        {
            if (_postUri == null) throw new ArgumentException("Post Uri not constructed");         
            _posts =  await client.GetFromJsonAsync<List<PostModel>>(_postUri);
            return _posts;
        }

        public async Task<ICollection<PostModel>> GetPostWithCommentsAsync()
        {
            if (_posts == null) await FetchPostsAsync();
            if (_comments == null) await FetchCommentsAsync();

            foreach (var post in _posts)
            {
                var c = _comments.Where(c => c.PostId == post.Id);
                if (c.Count() > 0) Debug.WriteLine($"Found {c.Count()} comments for post {post.Id}");
                post.Comments = c;
            }
            return _posts.ToList();
        }

        public async Task<ICollection<PostModel>> GetPostWithCommentsLinqAsync()
        {            
            if (_posts == null) await FetchPostsAsync();
            if (_comments == null) await FetchCommentsAsync();

            var res = _posts.Select(p => new PostModel()
            {
               Body = p.Body,
               Id = p.Id,   
               Title = p.Title,
               UserId = p.UserId,
               Comments = _comments.Where(c=> p.Id ==c.PostId)
            });

            return res.ToList();
        }
    }
}
