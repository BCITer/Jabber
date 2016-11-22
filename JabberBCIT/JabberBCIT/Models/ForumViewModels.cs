using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using System.Web;

namespace JabberBCIT.Models
{
    public class ForumPostsViewmodel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public IEnumerable<Subforum> Subforums { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public IEnumerable<ForumPost> Posts { get; set; }
    }

    public class ViewThreadViewModel
    {
        public ForumPost post { get; set; }
        public List<Comment> comments { get; set; }
    }
    public class CommentAndChildrenViewModel
    {
        public Comment comment { get; set; }
        public List<Comment> comments { get; set; }
    }
}