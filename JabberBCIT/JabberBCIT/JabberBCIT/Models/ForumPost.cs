namespace JabberBCIT
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ForumPost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ForumPost()
        {
            Comments = new HashSet<Comment>();
            Tags = new HashSet<Tag>();
        }

        [Key]
        public long PostID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        [Required]
        [StringLength(500)]
        public string PostTitle { get; set; }

        public string Message { get; set; }

        public short Votes { get; set; }

        public DateTime PostTimestamp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
