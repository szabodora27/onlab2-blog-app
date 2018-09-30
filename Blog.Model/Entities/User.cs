using Microsoft.AspNetCore.Identity;

namespace Blog.Model.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string JobTitle { get; set; }

        public string PictureUrl { get; set; }

        public string About { get; set; }
    }
}
