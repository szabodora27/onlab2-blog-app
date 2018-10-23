using Blog.Model.Entities;
using System;
namespace Blog.Model.ModelInterfaces
{
    public interface IAuditable<TKey> : IDbEntry<TKey> where TKey : struct, IEquatable<TKey>
    {
        string CreatedById { get; set; }

        ApplicationUser CreatedBy { get; set; }

        DateTime CreatedDate { get; set; }

        string LastModifiedById { get; set; }

        ApplicationUser LastModifiedBy { get; set; }

        DateTime? LastModifiedDate { get; set; }
    }
}
