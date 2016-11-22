using System.Collections.Generic;

namespace JabberBCIT.Models
{

    public class PostViewModel
    {
        public ForumPost post { get; set; }
        public string author { get; set; }
        public int votes { get; set; }
        public List<long> childCommentIDs { get; set; }
    }

    public class CommentViewModel
    {
        public Comment comment { get; set; }
        public string author { get; set; }
        public int votes { get; set; }
        public List<long> childCommentIDs { get; set; }
    }

}