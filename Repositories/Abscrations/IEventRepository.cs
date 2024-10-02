using meditationApp.Entities;

namespace meditationApp.Repositories.Abscrations;

public interface IEventRepository : IBaseRepository
{
    Task<Event> AddEventAsync(Event eventToAdd);
    Task<List<Event>> GetAllEventsAsync();
    Task<Event?> GetEventByIdAsync(int id);
}