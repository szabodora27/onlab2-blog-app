using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Bll.Dtos
{
    public class BlogPostIndexDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string FeaturedImageUrl { get; set; }

        public string CreatedBy { get; set; }
    }
}
