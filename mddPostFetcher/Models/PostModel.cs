using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace mddPostFetcher.Models
{
    public class PostModel
    {
        public PostModel()
        {
            Comments = new List<CommentModel>();
        }
        public int UserId { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        [JsonIgnore]
        public IEnumerable<CommentModel> Comments { get;set; }

    }
}
