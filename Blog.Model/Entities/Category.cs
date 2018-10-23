using System.Collections.Generic;

namespace Blog.Model.Entities
{
    public class Category
    {
        public Category()
        {
            BlogPosts = new HashSet<BlogPost>();
            Tags = new HashSet<Tag>();
        }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<BlogPost> BlogPosts { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}
