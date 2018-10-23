using System;

namespace Blog.Model.Entities
{
    public class Favorite
    {
        public int FavoriteId { get; set; }

        public Guid BlogPostId { get; set; }

        public BlogPost BlogPost { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
