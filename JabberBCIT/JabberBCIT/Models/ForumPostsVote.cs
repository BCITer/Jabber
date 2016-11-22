namespace JabberBCIT.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
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

        public virtual User User { get; set; }

        public virtual ForumPost ForumPost { get; set; }
    }
}
