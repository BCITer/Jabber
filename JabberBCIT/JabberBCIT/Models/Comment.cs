namespace JabberBCIT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comment()
        {
            CommentsVotes = new HashSet<CommentsVote>();
        }

        public long CommentID { get; set; }

        public long PostID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        [Column("Comment")]
        [Required]
        public string Text { get; set; }

        public long? ParentCommentID { get; set; }

        public DateTime PostTimestamp { get; set; }

        public int Hidden { get; set; }

        public virtual User User { get; set; }

        public virtual ForumPost ForumPost { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommentsVote> CommentsVotes { get; set; }
    }
}
