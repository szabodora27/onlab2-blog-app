using Blog.Model.ModelInterfaces;
using System;

namespace Blog.Model.Entities
{
    public class EntityBase : IAuditable<Guid>, ISoftDelete
    {
        public Guid Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Guid CreatedById { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public User CreatedBy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DateTime CreatedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Guid LastModifiedById { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public User LastModifiedBy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DateTime LastModifiedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsDeleted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
