using JabberBCIT.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
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


        /// <summary>
        /// This can fail if there's multiple queries at the same time, so we wrap the entire
        /// thing in a try/catch
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult NotificationPartial()
        {
            try
            {
                ChitterDbContext db = ChitterDbContext.Create;
                var id = User.Identity.GetUserId();
                // get all of them, technically sort by the commentid so newer ones are at the top, same thing as date
                var notifications = db.Notifications.Where(x => x.UserID == id).OrderByDescending((notification => notification.ObjectID)).ToList();
                var unseen = notifications.Sum(x => x.Seen);
                if (unseen > 0)
                { // set all to seen, just as fast as checking them all beforehand.. probably
                    notifications.ForEach(x => x.Seen = 0);
                    db.SaveChanges();
                }

                notifications = notifications.Take(5).ToList();
                
                return PartialView(new NotificationViewModel()
                {
                    notifications = notifications,
                    newNotifications = unseen,
                });
            }
            catch
            {
                return PartialView(new NotificationViewModel() // return blank, just in case.
                {
                    notifications = new List<Notification>(),
                    newNotifications = 0,
                });
            }
        }
    }
}