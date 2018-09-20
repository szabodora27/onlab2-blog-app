﻿using Blog.Model.Entities;
using System;
namespace Blog.Model.ModelInterfaces
{
    public interface IAuditable<TKey> : IDbEntry<TKey> where TKey : struct, IEquatable<TKey>
    {
        TKey CreatedById { get; set; }

        User CreatedBy { get; set; }

        DateTime CreatedDate { get; set; }

        TKey LastModifiedById { get; set; }

        User LastModifiedBy { get; set; }

        DateTime LastModifiedDate { get; set; }
    }
}