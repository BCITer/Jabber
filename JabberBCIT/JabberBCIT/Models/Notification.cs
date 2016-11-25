namespace JabberBCIT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Notification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long NotificationID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        //ForumPost/Comment/Chat/Message
        [Required]
        [StringLength(500)]
        public string Type { get; set; }

        //PostID/CommentID/ChatID/MessageID
        public long ObjectID { get; set; }

        //0 for no, 1 for yes
        public short Seen { get; set; }
    }
}
