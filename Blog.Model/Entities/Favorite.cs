﻿namespace Blog.Model.Entities
{
    public class Favorite
    {
        public int FavoriteId { get; set; }

        public int BlogPostId { get; set; }

        public BlogPost BlogPost { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
