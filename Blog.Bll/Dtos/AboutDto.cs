using System.Collections.Generic;

namespace Blog.Bll.Dtos
{
    public class AboutDto
    {
        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public string JobTitle { get; set; }

        public string About { get; set; }

        public IEnumerable<BlogPostIndexDto> Posts { get; set; }
    }
}
