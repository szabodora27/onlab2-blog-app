using System;

namespace Blog.Model.ModelInterfaces
{
    public interface IDbEntry<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}
