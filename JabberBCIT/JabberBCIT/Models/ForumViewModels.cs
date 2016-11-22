using System.Collections.Generic;

namespace JabberBCIT.Models
{
    public class ForumPostsViewmodel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public IEnumerable<ForumPost> Posts { get; set; }
    }

    public class ViewThreadViewModel
    {
        public ForumPost post { get; set; }
        public List<long> childCommentIDs { get; set; }
    }
    public class CommentViewModel
    {
        public Comment comment { get; set; }
        public List<long> childCommentIDs { get; set; }
    }
}