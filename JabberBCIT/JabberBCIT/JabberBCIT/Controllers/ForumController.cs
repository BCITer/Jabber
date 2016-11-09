using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JabberBCIT.Controllers
{
    public class ForumController : Controller
    {
        // GET: Forum
        public ActionResult ForumMain()
        {
            return View();
        }
    }
}