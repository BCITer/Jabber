namespace JabberBCIT
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ChitterContext : DbContext
    {
        public ChitterContext()
            : base("name=ChitterContext")
        {
        }

        public virtual DbSet<ChatConversation> ChatConversations { get; set; }
        public virtual DbSet<ChatMessage> ChatMessages { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<ForumPost> ForumPosts { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatConversation>()
                .Property(e => e.UserEmail)
                .IsUnicode(false);

            modelBuilder.Entity<ChatConversation>()
                .HasMany(e => e.ChatMessages)
                .WithRequired(e => e.ChatConversation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChatMessage>()
                .Property(e => e.UserEmail)
                .IsUnicode(false);

            modelBuilder.Entity<ChatMessage>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<Comment>()
                .Property(e => e.UserEmail)
                .IsUnicode(false);

            modelBuilder.Entity<Comment>()
                .Property(e => e.Comment1)
                .IsUnicode(false);

            modelBuilder.Entity<ForumPost>()
                .Property(e => e.UserEmail)
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

            modelBuilder.Entity<User>()
                .Property(e => e.UserEmail)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.DisplayName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.ProfilePicture)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ChatConversations)
                .WithRequired(e => e.User)
                .HasForeignKey(e => new { e.UserID, e.UserEmail })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ChatMessages)
                .WithRequired(e => e.User)
                .HasForeignKey(e => new { e.UserID, e.UserEmail })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.User)
                .HasForeignKey(e => new { e.UserID, e.UserEmail })
                .WillCascadeOnDelete(false);
        }

    }
}
