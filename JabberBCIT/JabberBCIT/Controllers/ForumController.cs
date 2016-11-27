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
        public ActionResult Index(string tag)
        {
            var listPostViewModel = new List<PostViewModel>();
            foreach (var p in db.ForumPosts.Where(p => p.Subforum.Name == tag))
            {
                listPostViewModel.Add(new PostViewModel()
                {
                    post = p,
                    votes = db.ForumPostsVotes.Where(x => x.PostID == p.PostID).Select(x => x.Value).AsEnumerable().Sum(x => x)
                });
            }
            ViewBag.ForumTitle = tag;
            return View(listPostViewModel);
        }

        public ActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePost(ForumPost post, string tag)
        {
            try
            {
                post.UserID = User.Identity.GetUserId();
                post.PostTimestamp = DateTime.Now;
                post.Subforum = db.Subforums.Where(x => x.Name == tag).FirstOrDefault();
                db.ForumPosts.Add(post);
                db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction(post.Subforum.Name, new { id = post.PostID });
        }

        public ActionResult CreateComment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateComment(Comment comment, long? commentID, long id)
        {
            try
            {
                comment.UserID = User.Identity.GetUserId();
                comment.PostTimestamp = DateTime.Now;
                comment.PostID = id;
                comment.ParentCommentID = commentID;
                db.Comments.Add(comment);
                db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("ViewThread");
        }

        public ActionResult ViewThread(int id)
        {
            PostViewModel viewModel = new PostViewModel();
            viewModel.post = db.ForumPosts.Find(id);
            viewModel.votes = db.ForumPostsVotes.Where(x => x.PostID == id).Select(x => x.Value).AsEnumerable().Sum(x => x);
            viewModel.childCommentIDs = db.Comments.Where(x => x.PostID == id && x.ParentCommentID == null).Select(x => x.CommentID).ToList();
            return View(viewModel);
        }

        public ActionResult VoteComment(int commentID, short value)
        {
            if (value == 1 || value == -1)
            {
                if (db.Comments.Any(x => x.CommentID == commentID))
                {
                    var oldVote = db.CommentsVotes.Find(commentID, User.Identity.GetUserId());
                    if (oldVote != null)
                    {
                        oldVote.Value = value;
                    }
                    else db.CommentsVotes.Add(new CommentsVote()
                    {
                        UserID = User.Identity.GetUserId(),
                        CommentID = commentID,
                        Value = value
                    });
                    db.SaveChanges();
                }
            }
            return RedirectToAction("ViewThread");
        }

        [ChildActionOnly]
        public ActionResult CommentPartial(int id)
        {
            var viewModel = new CommentViewModel();
            viewModel.comment = db.Comments.Find(id);
            viewModel.votes = db.CommentsVotes.Where(x => x.CommentID == id).Select(x => x.Value).AsEnumerable().Sum(x => x);
            viewModel.childCommentIDs = db.Comments.Where(x => x.ParentCommentID == id).Select(x => x.CommentID).ToList();
            return PartialView(viewModel);
        }

        [ChildActionOnly]
        public ActionResult SidebarPartial()
        {
            return PartialView(db.Subforums.ToList());
        }
    }
}