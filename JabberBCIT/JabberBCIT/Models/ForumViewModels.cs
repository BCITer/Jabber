using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using System.Web;

namespace JabberBCIT.Models
{
    public class ViewThreadViewModel
    {
        public ForumPost post { get; set; }
        public List<Comment> comments { get; set; }
    }
}