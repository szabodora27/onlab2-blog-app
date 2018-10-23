using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Model.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Favorites = new HashSet<Favorite>();
            BlogPosts = new HashSet<BlogPost>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string JobTitle { get; set; }

        public string PictureUrl { get; set; }

        public string About { get; set; }
        
        public IEnumerable<Favorite> Favorites { get; set; }
        
        public IEnumerable<BlogPost> BlogPosts { get; set; }
    }
}
