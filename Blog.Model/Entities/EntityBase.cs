using Blog.Model.ModelInterfaces;
using System;

namespace Blog.Model.Entities
{
    public class EntityBase : IAuditable<Guid>, ISoftDelete
    {
        public Guid Id { get; set; }

        public string CreatedById { get; set; }

        public ApplicationUser CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string LastModifiedById { get; set; }

        public ApplicationUser LastModifiedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
