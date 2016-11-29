using System;
using System.Collections.Generic;

namespace JabberBCIT.Models
{
    public class PostViewModel
    {
        public ForumPost post { get; set; }
        public int votes { get; set; }
        public DateTime PostTimestamp { get; set; }
        public List<CommentViewModel> childComments { get; set; }
    }

    public class CommentViewModel
    {
        public Comment comment { get; set; }
        public int votes { get; set; }
        public int hidden { get; set; }
        public List<CommentViewModel> childComments { get; set; }
    }
}