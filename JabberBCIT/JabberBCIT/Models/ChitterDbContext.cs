namespace JabberBCIT
{
    using Models;
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;

    public partial class ChitterDbContext : IdentityDbContext<User>
    {
        private ChitterDbContext() : base("name=ChitterContext")
        { }

        private static ChitterDbContext db;

        public virtual DbSet<ChatConversation> ChatConversations { get; set; }
        public virtual DbSet<ChatMessage> ChatMessages { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<CommentsVote> CommentsVotes { get; set; }
        public virtual DbSet<ForumPost> ForumPosts { get; set; }
        public virtual DbSet<ForumPostsVote> ForumPostsVotes { get; set; }
        public virtual DbSet<Subforum> Subforums { get; set; }

        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<NewNotification> NewNotifications { get; set; }

        public static ChitterDbContext Create()
        {
            get
            {
                if (db == null)
                {
                    db = new ChitterDbContext();
                }
                return db;
            }
        }

        public static ChitterDbContext dontUseThis()
        {
            return new ChitterDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.ChatConversations)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ChatMessages)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.CommentsVotes)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ForumPosts)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ForumPostsVotes)
                .WithRequired(e => e.User)
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
                .Property(e => e.Text)
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

            modelBuilder.Entity<Subforum>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Subforum>()
                .HasMany(e => e.ForumPosts)
                .WithRequired(e => e.Subforum)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subforum>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Subforums)
                .Map(m => m.ToTable("UserSubforums").MapLeftKey("SubforumID").MapRightKey("UserID"));

            modelBuilder.Entity<Notification>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<NewNotification>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<IdentityUserLogin>().HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey(x => x.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(x => new { x.RoleId, x.UserId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
