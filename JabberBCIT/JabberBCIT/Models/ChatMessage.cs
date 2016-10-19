namespace JabberBCIT
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ChatMessage
    {
        [Key]
        public long MessageID { get; set; }

        public long ChatID { get; set; }

        public long UserID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserEmail { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime Timestamp { get; set; }

        public virtual ChatConversation ChatConversation { get; set; }

        public virtual User User { get; set; }
    }
}
