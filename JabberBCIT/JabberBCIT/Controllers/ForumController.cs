using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JabberBCIT.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace JabberBCIT.Controllers
{
    // [Authorize] Uncommenting this makes it so you have to login to view the forums
    public class ForumController : Controller
    {
        ChitterDbContext db = ChitterDbContext.Create;

        // GET: Forum
        public ActionResult Index(string tag = "Global")
        {
            ForumPostsViewmodel p = new ForumPostsViewmodel();

            // get the forum posts from this subforum
            p.Posts = (from post in db.ForumPosts where post.Subforum.Name == tag select post).ToList();
            p.Subforums = db.Subforums.ToList();
            ViewBag.ForumTitle = tag;

            return View(p);
        }

        public ActionResult CreateForumPost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateForumPost(ForumPost post, string tag = "Global")
        {
            ViewThreadViewModel model = new ViewThreadViewModel();
            post.UserID = User.Identity.GetUserId();
            post.PostTimestamp = DateTime.Now;

            try
            {
                Subforum s = (from subforum in db.Subforums where subforum.Name == tag select subforum).FirstOrDefault();
                //post.Subforum = s;

                db.ForumPosts.Add(post);
                db.SaveChanges();
                model.post = post;
                model.comments = db.Comments.Where(x => x.PostID == post.PostID).ToList();
                return View("ViewForumThread", model);
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
            return Index();
        }

        public ActionResult ViewForumThread(int? id)
        {
            ViewThreadViewModel model = new ViewThreadViewModel();
            model.post = db.ForumPosts.Find(id);
            model.comments = db.Comments.Where(x => x.PostID == id).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult ForumPostPartial(ForumPost p)
        {
            return PartialView();
        }
    }
}