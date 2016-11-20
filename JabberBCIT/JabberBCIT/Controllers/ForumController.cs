using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JabberBCIT.Controllers
{
    // [Authorize] Uncommenting this makes it so you have to login to view the forums
    public class ForumController : Controller
    {
        ChitterDbContext db = new ChitterDbContext();
        // GET: Forum
        public ActionResult Index(string tag = "Global")
        {
            ForumPostsViewmodel p = new ForumPostsViewmodel();
            //Tag t = new JabberBCIT.Tag
            //{
            //    Tag1 = tag
            //};
            //p.Posts = (from ForumPost in db.ForumPosts where ForumPost.Tags.Contains(t) select new ForumPost());

            p.Posts = db.ForumPosts.ToList();

            return View(p);
        }

        public ActionResult CreateForumPost()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult CreateForumPost(ForumPost post, string tag = "Global")
        {
            post.UserID = User.Identity.GetUserId();
            post.PostTimestamp = DateTime.Now;
            Tag t = new Tag();
            t.Tag1 = tag;
            t.ForumPost = post;
            post.Tags.Add(t);

            db.ForumPosts.Add(post);
            db.SaveChanges();

            return View();
        }

        public ActionResult ViewForumThread()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForumPostPartial(ForumPost p)
        {
            return PartialView();
        }
    }
}