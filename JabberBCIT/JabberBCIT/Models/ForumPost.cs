namespace JabberBCIT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ForumPost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ForumPost()
        {
            Comments = new HashSet<Comment>();
            ForumPostsVotes = new HashSet<ForumPostsVote>();
        }

        [Key]
        public long PostID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Title")]
        public string PostTitle { get; set; }

        public string Message { get; set; }

        public DateTime PostTimestamp { get; set; }

        public long SubforumID { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual Subforum Subforum { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForumPostsVote> ForumPostsVotes { get; set; }
    }
}
