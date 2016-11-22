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
            ForumPostsViewmodel viewModel = new ForumPostsViewmodel();

            // get the forum posts from this subforum
            viewModel.Posts = db.ForumPosts.Where(x => x.Subforum.Name == tag).ToList();
            viewModel.Subforums = db.Subforums.ToList();
            ViewBag.ForumTitle = tag;

            return View(viewModel);
        }

        public ActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePost(ForumPost post, string tag = "Global")
        {
            try
            {
                // post.UserID = User.Identity.GetUserId();
                post.UserID = "b0394e3f-3a78-44eb-a2be-a60bb318ef3d";
                post.PostTimestamp = DateTime.Now;
                post.Subforum = db.Subforums.Where(x => x.Name == tag).FirstOrDefault();
                db.ForumPosts.Add(post);
                db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("ViewThread", new { id = post.PostID});
        }

        public ActionResult ViewThread(int? id)
        {
            ViewThreadViewModel viewModel = new ViewThreadViewModel();
            viewModel.post = db.ForumPosts.Find(id);
            viewModel.comments = db.Comments.Where(x => x.PostID == id).ToList();
            return View(viewModel);
        }

    }
}