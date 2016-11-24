using System.Web.Mvc;
using System.Web.Routing;

namespace JabberBCIT
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.AppendTrailingSlash = true;

            routes.MapRoute(
                name: "CreatePostInSubForum",
                url: "Forum/{tag}/CreatePost",
                defaults: new { controller = "Forum", action = "CreatePost"}
            );
            routes.MapRoute(
                name: "VoteComment",
                url: "Forum/{tag}/{id}/Vote/{commentID}/{value}",
                defaults: new { controller = "Forum", action = "VoteComment" }
            );
            routes.MapRoute(
                name: "ViewThread",
                url: "Forum/{tag}/{id}",
                defaults: new { controller = "Forum", action = "ViewThread"}
            );
            routes.MapRoute(
                name: "ViewForum",
                url: "Forum/{tag}",
                defaults: new { controller = "Forum", action = "Index"}
            );

            routes.MapRoute(
                name: "ReplyComment",
                url: "Forum/{tag}/{id}/{commentID}/Reply",
                defaults: new { controller = "Forum", action = "CreateComment" }
            );

            routes.MapRoute(
                name: "ReplyThread",
                url: "Forum/{tag}/{id}/Reply",
                defaults: new { controller = "Forum", action = "CreateComment"}
            );
            
            routes.MapRoute(
                name: "EditProfile",
                url: "Profile/Edit",
                defaults: new { controller = "Manage", action = "Edit" }
            );

            routes.MapRoute(
                name: "Profile",
                url: "Profile/{id}",
                defaults: new { controller = "Manage", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
