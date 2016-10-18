namespace JabberBCIT
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            ChatConversations = new HashSet<ChatConversation>();
            ChatMessages = new HashSet<ChatMessage>();
            Comments = new HashSet<Comment>();
        }

        [Key]
        [Column(Order = 0)]
        public long UserID { get; set; }

        [Key]
        [Column(Order = 1)]
        public string UserEmail { get; set; }

        [Required]
        [StringLength(128)]
        public string Password { get; set; }

        [Required]
        [StringLength(128)]
        public string DisplayName { get; set; }

        [StringLength(128)]
        public string ProfilePicture { get; set; }

        [Column(TypeName = "date")]
        public DateTime JoinDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChatConversation> ChatConversations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChatMessage> ChatMessages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
