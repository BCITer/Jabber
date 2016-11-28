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
            if (db.Subforums.Any(x => x.Name == tag))
            {
                var listPostViewModel = new List<PostViewModel>();
                foreach (var p in db.ForumPosts.Where(p => p.Subforum.Name == tag))
                {
                    listPostViewModel.Add(new PostViewModel()
                    {
                        post = p,
                        PostTimestamp = p.PostTimestamp,
                        votes = p.ForumPostsVotes.Sum(x => x.Value)
                    });
                }
                ViewBag.ForumTitle = tag;

                // DO YOUR SORTING IN FOLLOWING METHOD//
                listPostViewModel.Sort((post1, post2) => sortFunction(post1, post2))

                return View(listPostViewModel);
            }
            return new EmptyResult();
        }

        public int sortFunction(PostViewModel p1, PostViewModel p2)
        {
            int compare = p2.votes.CompareTo(p1.votes);
            if (compare == 0)
            {
                return p2.PostTimestamp.CompareTo(p1.PostTimestamp);
            }
            return compare;
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
                return new EmptyResult();
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
                return new EmptyResult();
            }
            return RedirectToAction("ViewThread");
        }

        public ActionResult ViewThread(long id)
        {
            if (db.ForumPosts.Any(x => x.PostID == id))
            {
                PostViewModel viewModel = new PostViewModel();
                viewModel.post = db.ForumPosts.Find(id);
                viewModel.votes = viewModel.post.ForumPostsVotes.Sum(x => x.Value);
                viewModel.childComments = getCommentTree(id);
                
                return View(viewModel);
            }
            return new EmptyResult();
        }

        List<CommentViewModel> getCommentTree(long basePostID)
        {
            List<CommentViewModel> model = new List<CommentViewModel>();
            
            // create commentviewmodels for every comment in this thread
            foreach (var comment in db.Comments.Where(x => x.PostID == basePostID).ToList())
            {
                model.Add(new CommentViewModel()
                {
                    votes = comment.CommentsVotes.Sum(x => x.Value),
                    comment = comment,
                    childComments = new List<CommentViewModel>(),
                });
            }

            // DO YOUR SORTING HERE //
            model.Sort((comment1, comment2) => comment2.votes.CompareTo(comment1.votes));
            // DO YOUR SORTING HERE //

            model.ForEach(i => i.childComments = model.Where(ch => ch.comment.ParentCommentID == i.comment.CommentID).ToList());
            return model.Where(x => x.comment.ParentCommentID == null).ToList();
        }
        
        public void VoteComment(long id, short value)
        {
            if (value == 1 || value == -1)
            {
                if (db.Comments.Any(x => x.CommentID == id))
                {
                    var oldVote = db.CommentsVotes.Find(id, User.Identity.GetUserId());
                    if (oldVote != null)
                    {
                        oldVote.Value = value;
                    }
                    else db.CommentsVotes.Add(new CommentsVote()
                    {
                        UserID = User.Identity.GetUserId(),
                        CommentID = id,
                        Value = value
                    });
                    db.SaveChanges();
                }
            }
        }

        public void VotePost(long id, short value)
        {
            if (value == 1 || value == -1)
            {
                if (db.ForumPosts.Any(x => x.PostID == id))
                {
                    var oldVote = db.ForumPostsVotes.Find(User.Identity.GetUserId(), id);
                    if (oldVote != null)
                    {
                        oldVote.Value = value;
                    }
                    else db.ForumPostsVotes.Add(new ForumPostsVote()
                    {
                        UserID = User.Identity.GetUserId(),
                        PostID = id,
                        Value = value
                    });
                    db.SaveChanges();
                }
            }
        }

        [ChildActionOnly]
        public ActionResult SidebarPartial()
        {
            return PartialView(db.Subforums.ToList());
        }
    }
}