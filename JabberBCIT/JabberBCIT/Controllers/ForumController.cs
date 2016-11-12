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
        // GET: Forum
        public ActionResult ForumMain()
        {
            return View();
        }

        public ActionResult CreateForumPost()
        {
            return View();
        }

        //public ActionResult CreateForumPost(ForumPost post)
        //{
        //    db.ForumPosts.Add(post);
        //    db.SaveChanges();

        //    return View
        //}

        public ActionResult ViewForumThread()
        {

            return View();
        }
    }
}