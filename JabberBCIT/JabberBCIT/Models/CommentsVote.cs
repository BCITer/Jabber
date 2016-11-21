namespace JabberBCIT.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class CommentsVote
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long CommentID { get; set; }

        [Key]
        [Column(Order = 1)]
        public string UserID { get; set; }

        public short Value { get; set; }

        public virtual User User { get; set; }

        public virtual Comment Comment { get; set; }
    }
}
