namespace JabberBCIT
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tag
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PostID { get; set; }

        [Key]
        [Column("Tag", Order = 1)]
        [StringLength(20)]
        public string Tag1 { get; set; }

        public virtual ForumPost ForumPost { get; set; }
    }
}
