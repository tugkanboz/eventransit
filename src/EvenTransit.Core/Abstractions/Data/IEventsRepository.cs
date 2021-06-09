using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EvenTransit.Core.Entities;

namespace EvenTransit.Core.Abstractions.Data
{
    public interface IEventsRepository
    {
        Task<List<Event>> GetEventsAsync();
        Task<Event> GetEventAsync(Expression<Func<Event, bool>> predicate);
        Entities.Service GetServiceByEvent(string eventName, string serviceName);
        Task<Entities.Service> GetServiceByEventAsync(string eventName, string serviceName);
        Task AddServiceToEvent(string eventId, Entities.Service serviceData);
        Task UpdateServiceOnEvent(string eventId, Entities.Service serviceData);
    }
}