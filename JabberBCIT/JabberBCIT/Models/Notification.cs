namespace JabberBCIT.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Notification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NotificationID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        [Required]
        [StringLength(500)]
        ///ForumPost/Comment/Chat/Message
        public string Type { get; set; }

        ///PostID/CommentID/ChatID/MessageID
        public long ObjectID { get; set; }

        ///1 for no, 0 for yes
        public short Seen { get; set; }
    }
}
