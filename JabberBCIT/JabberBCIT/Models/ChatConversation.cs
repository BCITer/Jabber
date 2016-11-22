namespace JabberBCIT.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    [Table("ChatConversation")]
    public partial class ChatConversation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChatConversation()
        {
            ChatMessages = new HashSet<ChatMessage>();
        }

        [Key]
        public long ChatID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        public long LastMessageSeenID { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChatMessage> ChatMessages { get; set; }
    }
}
