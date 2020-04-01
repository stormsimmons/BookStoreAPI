using System;

namespace BookStore.Domain.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
