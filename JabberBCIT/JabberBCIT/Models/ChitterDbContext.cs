namespace JabberBCIT
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public partial class ChitterDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<ChatConversation> ChatConversations { get; set; }
        public virtual DbSet<ChatMessage> ChatMessages { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<ForumPost> ForumPosts { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        public ChitterDbContext()
            : base("name=ChitterContext", throwIfV1Schema: false)
        {
        }

        public static ChitterDbContext Create()
        {
            return new ChitterDbContext();
        }

      /*  protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatConversation>()
                .HasMany(e => e.ChatMessages)
                .WithRequired(e => e.ChatConversation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChatMessage>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<Comment>()
                .Property(e => e.Comment1)
                .IsUnicode(false);

            modelBuilder.Entity<ForumPost>()
                .Property(e => e.PostTitle)
                .IsUnicode(false);

            modelBuilder.Entity<ForumPost>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<ForumPost>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.ForumPost)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ForumPost>()
                .HasMany(e => e.Tags)
                .WithRequired(e => e.ForumPost)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tag>()
                .Property(e => e.Tag1)
                .IsUnicode(false);

            base.OnModelCreating(modelBuilder);
        }*/
    }
}