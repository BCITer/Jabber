namespace JabberBCIT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatConversation",
                c => new
                    {
                        ChatID = c.Long(nullable: false, identity: true),
                        UserID = c.String(nullable: false, maxLength: 128),
                        LastMessageSeenID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ChatID);
            
            CreateTable(
                "dbo.ChatMessages",
                c => new
                    {
                        MessageID = c.Long(nullable: false, identity: true),
                        ChatID = c.Long(nullable: false),
                        UserID = c.String(nullable: false, maxLength: 128),
                        Message = c.String(nullable: false, unicode: false),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MessageID)
                .ForeignKey("dbo.ChatConversation", t => t.ChatID)
                .Index(t => t.ChatID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentID = c.Long(nullable: false, identity: true),
                        PostID = c.Long(nullable: false),
                        UserID = c.String(nullable: false, maxLength: 128),
                        Comment = c.String(nullable: false, unicode: false),
                        ParentCommentID = c.Long(),
                        Votes = c.Short(nullable: false),
                        PostTimestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.ForumPosts", t => t.PostID)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.ForumPosts",
                c => new
                    {
                        PostID = c.Long(nullable: false, identity: true),
                        UserID = c.String(nullable: false, maxLength: 128),
                        PostTitle = c.String(nullable: false, maxLength: 500, unicode: false),
                        Message = c.String(unicode: false),
                        Votes = c.Short(nullable: false),
                        PostTimestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PostID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        PostID = c.Long(nullable: false),
                        Tag = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => new { t.PostID, t.Tag })
                .ForeignKey("dbo.ForumPosts", t => t.PostID)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ProfilePicture = c.String(),
                        JoinDate = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Tags", "PostID", "dbo.ForumPosts");
            DropForeignKey("dbo.Comments", "PostID", "dbo.ForumPosts");
            DropForeignKey("dbo.ChatMessages", "ChatID", "dbo.ChatConversation");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Tags", new[] { "PostID" });
            DropIndex("dbo.Comments", new[] { "PostID" });
            DropIndex("dbo.ChatMessages", new[] { "ChatID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Tags");
            DropTable("dbo.ForumPosts");
            DropTable("dbo.Comments");
            DropTable("dbo.ChatMessages");
            DropTable("dbo.ChatConversation");
        }
    }
}
