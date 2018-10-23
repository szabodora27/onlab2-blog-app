using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Model.Entities
{
    public class BlogPost : EntityBase
    {
        public BlogPost()
        {
            Comments = new HashSet<Comment>();
            Favorites = new HashSet<Favorite>();
        }

        [Required]
        public string Title { get; set; }

        public string FeaturedImageUrl { get; set; }

        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        [Required]
        public string Content { get; set; }

        public string Attachments { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public IEnumerable<Favorite> Favorites { get; set; }
    }
}
