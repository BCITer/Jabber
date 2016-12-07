using System.Web;
using System.Web.Optimization;

namespace JabberBCIT {
    public class BundleConfig {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/HomeGlobal").Include(
                     "~/Scripts/HomeGlobal.js"));
             bundles.Add(new ScriptBundle("~/bundles/forum").Include(
                   "~/Scripts/forum.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/login").Include(
                     "~/Content/login.css"));            
            bundles.Add(new StyleBundle("~/Content/home").Include(
                    "~/Content/Home.less"));
            bundles.Add(new StyleBundle("~/Content/register").Include(
                    "~/Content/register.css"));
            bundles.Add(new StyleBundle("~/Content/notification").Include(
                    "~/Content/Notification.css"));
            
        }
    }
}
