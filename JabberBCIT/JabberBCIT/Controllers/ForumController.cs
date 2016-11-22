using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JabberBCIT.Models;

namespace JabberBCIT.Controllers
{
    // [Authorize] Uncommenting this makes it so you have to login to view the forums
    public class ForumController : Controller
    {
        ChitterDbContext db = ChitterDbContext.Create;

        // GET: Forum
        public ActionResult Index(string tag = "Global")
        {
            ViewBag.ForumTitle = tag;
            return View(db.ForumPosts.Where(x => x.Subforum.Name == tag).Select(x => x.PostID).ToList());
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
                post.UserID = User.Identity.GetUserId();
                //post.UserID = "b0394e3f-3a78-44eb-a2be-a60bb318ef3d";
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

        public ActionResult ViewThread(int id)
        {
            PostViewModel viewModel = new PostViewModel();
            viewModel.post = db.ForumPosts.Find(id);
            viewModel.author = db.Users.Find(viewModel.post.UserID).UserName; 
            viewModel.votes = db.ForumPostsVotes.Where(x => x.PostID == id).Select(x => x.Value).AsEnumerable().Sum(x => x);
            viewModel.childCommentIDs = db.Comments.Where(x => x.PostID == id && x.ParentCommentID == null).Select(x => x.CommentID).ToList();
            return View(viewModel);
        }

        [ChildActionOnly]
        public ActionResult CommentPartial(int id)
        {
            var viewModel = new CommentViewModel();
            viewModel.comment = db.Comments.Find(id);
            viewModel.author = db.Users.Find(viewModel.comment.UserID).UserName;
            viewModel.votes = db.CommentsVotes.Where(x => x.CommentID == id).Select(x => x.Value).AsEnumerable().Sum(x => x);
            viewModel.childCommentIDs = db.Comments.Where(x => x.ParentCommentID == id).Select(x => x.CommentID).ToList();
            return PartialView(viewModel);
        }

        [ChildActionOnly]
        public ActionResult SidebarPartial()
        {
            return PartialView(db.Subforums.ToList());
        }

        [ChildActionOnly]
        public ActionResult PostPartial(int id)
        {
            PostViewModel viewModel = new PostViewModel();
            viewModel.post = db.ForumPosts.Find(id);
            viewModel.author = db.Users.Find(viewModel.post.UserID).UserName;
            viewModel.votes = db.ForumPostsVotes.Where(x => x.PostID == id).Select(x => x.Value).AsEnumerable().Sum(x => x);
            return PartialView(viewModel);
        }

    }
}