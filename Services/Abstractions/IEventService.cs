using meditationApp.DTO.events;
using meditationApp.Entities;
using meditationApp.Helpers;

namespace meditationApp.Services.Abstractions;

public interface IEventService
{
    Task<Result<Event>> AddEventAsync(CreateEventDTO eventToAdd);

    Task<Result<List<EventResponseDTO>>> GetAllEventsAsync();

    Task<Result<EventResponseDTO>> GetEventByIdAsync(int id);
}