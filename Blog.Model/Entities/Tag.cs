namespace Blog.Model.Entities
{
    public class Tag
    {
        public int TagId { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
