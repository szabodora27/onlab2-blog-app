using Blog.Model.Entities;
using System;

namespace Blog.Bll.Dtos
{
    public class PostDetailDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string FeaturedImageUrl { get; set; }

        public string Content { get; set; }

        public string CreatorId { get; set; }

        public string CreatorName { get; set; }

        public bool IsFavorite { get; set; }
    }
}
