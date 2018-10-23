using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Bll.Dtos
{
    public class CommentDto
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string CreatorName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
