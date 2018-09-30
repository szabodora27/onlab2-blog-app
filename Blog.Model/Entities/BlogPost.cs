using System;

namespace Blog.Model.Entities
{
    public class BlogPost : EntityBase
    {
        public string Title { get; set; }

        public string FeaturedImage { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int CreatedById { get; set; }

        public ApplicationUser CreatedBy { get; set; }

        public string Content { get; set; }

        public string Attachments { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
