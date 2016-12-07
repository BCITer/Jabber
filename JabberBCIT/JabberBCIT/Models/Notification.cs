namespace JabberBCIT.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class Notification
    {
        public Notification()
        {
            Seen = 1; // always need to set it to unseen
        }

        public long NotificationID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        /// <summary>
        /// forumpost/comment/chat/message
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Type { get; set; }

        /// <summary>
        /// postid/commentid/chatid/messageid
        /// </summary>
        [StringLength(500)]
        public string ObjectID { get; set; }
        
        /// <summary>
        /// 1 for no, 0 for yes, so we can sum the amount that are unseen
        /// </summary>
        public short Seen { get; set; }

        /// <summary>
        /// trim the blurb of the reply to fit it in
        /// </summary>
        [StringLength(500)]
        public string Text { get; set; }
    }
}
