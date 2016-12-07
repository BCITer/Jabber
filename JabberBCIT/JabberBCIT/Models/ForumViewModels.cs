using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JabberBCIT.Models
{
	public class CreateSubForumViewModel
    {
        [DisplayName("Topic")]
        [Required]
        public string Name { get; set; }
    }
	
    public class PostViewModel
    {
        public ForumPost post { get; set; }
        public string author { get; set; }
        public int votes { get; set; }
        public DateTime PostTimestamp { get; set; }
        public List<CommentViewModel> childComments { get; set; }
    }

    public class CommentViewModel
    {
        public Comment comment { get; set; }
        public string author { get; set; }
        public int votes { get; set; }
        public int hidden { get; set; }
        public List<CommentViewModel> childComments { get; set; }
    }
}