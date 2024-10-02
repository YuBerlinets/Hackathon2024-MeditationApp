using meditationApp.Data;
using meditationApp.Entities;
using meditationApp.Repositories.Abscrations;
using Microsoft.EntityFrameworkCore;

namespace meditationApp.Repositories;

public class EventRepository : BaseRepository, IEventRepository

{
    public EventRepository(StoreContext dbContext) : base(dbContext)
    {
    }

    public async Task<Event> AddEventAsync(Event eventToAdd)
    {
        await _dbContext.Events.AddAsync(eventToAdd);
        await _dbContext.SaveChangesAsync();
        return eventToAdd;
    }

    public Task<List<Event>> GetAllEventsAsync()
    {
        return _dbContext.Events.ToListAsync();
    }

    public async Task<Event?> GetEventByIdAsync(int id)
    {
        return await _dbContext.Events.FindAsync(id);
    }
}