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
        ChitterDbContext db = ChitterDbContext.Create();
        // GET: Forum
        public ActionResult ForumMain()
        {
            return View(db.ForumPosts.ToList());
        }

        public ActionResult CreateForumPost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateForumPost(ForumPost post)
        {
            post.UserID = User.Identity.ToString();
            post.PostTimestamp = DateTime.Now;
            post.Votes = 0;

            db.ForumPosts.Add(post);
            db.SaveChanges();

            return View();
        }

        public ActionResult ViewForumThread()
        {
            return View();
        }
    }
}