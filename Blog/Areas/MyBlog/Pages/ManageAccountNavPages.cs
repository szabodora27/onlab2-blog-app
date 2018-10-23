using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Blog.Areas.MyBlog.Pages
{
    public static class ManageAccountNavPages
    {
        public static string BlogPosts => "Posts";

        public static string Favorites => "Favorites";

        public static string Profil => "Index";

        public static string BlogPostsNavClass(ViewContext viewContext) => PageNavClass(viewContext, BlogPosts);

        public static string FavoritesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Favorites);

        public static string ProfilNavClass(ViewContext viewContext) => PageNavClass(viewContext, Profil);

        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
