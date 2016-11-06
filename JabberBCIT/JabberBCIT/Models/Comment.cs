namespace JabberBCIT
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comment
    {
        public long CommentID { get; set; }

        public long PostID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        [Column("Comment")]
        [Required]
        public string Comment1 { get; set; }

        public long? ParentCommentID { get; set; }

        public short Votes { get; set; }

        public DateTime PostTimestamp { get; set; }

        public virtual ForumPost ForumPost { get; set; }
    }
}
