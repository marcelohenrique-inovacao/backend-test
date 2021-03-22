using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backendtest.Shared.Messages;

namespace backendtest.Shared.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {
        Task SalvarEvento<TEvent>(TEvent evento) where TEvent : Event;
        Task<IEnumerable<StoredEvent>> ObterEventos(Guid aggregateId);
    }
}