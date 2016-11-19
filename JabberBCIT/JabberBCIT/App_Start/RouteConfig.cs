using System.Web.Mvc;
using System.Web.Routing;

namespace JabberBCIT
{
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "SpecificForum",
                url: "Forum/{tag}",
                defaults: new { controller = "Forum", action = "Index",  tag = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "CreateGlobalForumPost",
                url: "Forum/CreateForumPost",
                defaults: new { controller = "Forum", action = "CreateForumPost", tag = "Global" }
            );

            routes.MapRoute(
                name: "Forum",
                url: "Forum/{tag}/CreateForumPost",
                defaults: new { controller = "Forum", action = "CreateForumPost", tag = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "EditProfile",
                url: "Profile/Edit/{id}",
                defaults: new { controller = "Manage", action = "Edit" }
            );

            routes.MapRoute(
                name: "Profile",
                url: "Profile/{id}",
                defaults: new { controller = "Manage", action = "Index"}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
