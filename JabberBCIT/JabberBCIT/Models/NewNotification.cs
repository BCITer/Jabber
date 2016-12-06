namespace JabberBCIT.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    //This only shows distinct new messages (no duplicates) (seen == 0)
    public partial class NewNotification
    {
        [Key]
        [Column(Order = 0)]
        public string UserID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(500)]
        public string Type { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ObjectID { get; set; }
    }
}
