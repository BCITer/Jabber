using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JabberBCIT.Controllers
{
    public class ForumController : Controller
    {
        public ChitterDbContext db = ChitterDbContext.Create();

        // GET: Forum
        public ActionResult ForumMain()
        {
            return View(db.ForumPosts.ToList());
        }
    }
}