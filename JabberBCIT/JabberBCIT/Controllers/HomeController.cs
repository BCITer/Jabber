using JabberBCIT.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace JabberBCIT.Controllers {

    [RequireHttps]
    public class HomeController : Controller {

        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult NotificationPartial()
        {
            ChitterDbContext db = ChitterDbContext.Create;
            var id = User.Identity.GetUserId();
            // get all of them, technically sort by the commentid so newer ones are at the top, same thing as date
            var notifications = db.Notifications.Where(x => x.UserID == id).OrderBy((notification => notification.ObjectID)).Take(5).ToList();
            var unseen = notifications.Sum(x => x.Seen);

            notifications.ForEach(x => x.Seen = 0); // set all to seen
            db.SaveChanges();

            NotificationViewModel viewModel = new NotificationViewModel()
            {
                notifications = notifications,
                newNotifications = unseen,
            };

            return PartialView(viewModel);
        }
    }
}