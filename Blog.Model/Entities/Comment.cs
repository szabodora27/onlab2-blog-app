namespace Blog.Model.Entities
{
    public class Comment : EntityBase
    {
        public int BlogPostId { get; set; }

        public BlogPost BlogPost { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public string Text { get; set; }
    }
}
