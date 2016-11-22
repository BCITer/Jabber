namespace JabberBCIT.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    
    public partial class ChatMessage
    {
        [Key]
        public long MessageID { get; set; }

        public long ChatID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime Timestamp { get; set; }

        public virtual User User { get; set; }

        public virtual ChatConversation ChatConversation { get; set; }
    }
}
