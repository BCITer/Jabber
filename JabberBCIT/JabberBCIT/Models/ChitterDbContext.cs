namespace JabberBCIT
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public partial class ChitterDbContext : IdentityDbContext<ApplicationUser>
    {
        public ChitterDbContext()
            : base("name=ChitterContext")
        {
        }

        public virtual DbSet<ChatConversation> ChatConversations { get; set; }
        public virtual DbSet<ChatMessage> ChatMessages { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<CommentsVote> CommentsVotes { get; set; }
        public virtual DbSet<ForumPost> ForumPosts { get; set; }
        public virtual DbSet<ForumPostsVote> ForumPostsVotes { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        public static ChitterDbContext Create()
        {
            return new ChitterDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.ChatConversations)
                .WithRequired(e => e.ApplicationUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.ChatMessages)
                .WithRequired(e => e.ApplicationUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.ApplicationUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.CommentsVotes)
                .WithRequired(e => e.ApplicationUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.ForumPosts)
                .WithRequired(e => e.ApplicationUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.ForumPostsVotes)
                .WithRequired(e => e.ApplicationUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

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

            modelBuilder.Entity<Comment>()
                .HasMany(e => e.CommentsVotes)
                .WithRequired(e => e.Comment)
                .WillCascadeOnDelete(false);

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
                .HasMany(e => e.ForumPostsVotes)
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
        }
    }
}
