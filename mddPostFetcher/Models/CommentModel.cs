﻿namespace mddPostFetcher.Models
{
    public class CommentModel
    {
        public long PostId { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
    }
}
