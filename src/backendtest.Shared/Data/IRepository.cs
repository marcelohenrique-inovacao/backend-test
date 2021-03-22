using System;
using backendtest.Shared.DomainObjects;

namespace backendtest.Shared.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}