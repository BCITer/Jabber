namespace JabberBCIT
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ForumPostsVote
    {
        [Key]
        [Column(Order = 0)]
        public string UserID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PostID { get; set; }

        public short Value { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ForumPost ForumPost { get; set; }
    }
}
