using AutoMapper;
using meditationApp.DTO.events;
using meditationApp.Entities;
using meditationApp.Helpers;
using meditationApp.Repositories.Abscrations;
using meditationApp.Services.Abstractions;
using Microsoft.IdentityModel.Tokens;

namespace meditationApp.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    private IMapper _mapper;

    public EventService(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<Result<Event>> AddEventAsync(CreateEventDTO eventToAdd)
    {
        var eventEntity = _mapper.Map<Event>(eventToAdd);

        var addedEvent = await _eventRepository.AddEventAsync(eventEntity);

        return Result<Event>.Success(addedEvent);
    }

    public async Task<Result<List<EventResponseDTO>>> GetAllEventsAsync()
    {
        var events = await _eventRepository.GetAllEventsAsync();

        if (events.IsNullOrEmpty())
            return Result<List<EventResponseDTO>>.Success([]);

        var response = events.Select(e => _mapper.Map<EventResponseDTO>(e)).ToList();

        return Result<List<EventResponseDTO>>.Success(response);
    }

    public async Task<Result<EventResponseDTO>> GetEventByIdAsync(int id)
    {
        var eventEntity = await _eventRepository.GetEventByIdAsync(id);

        if (eventEntity == null)
            return Result<EventResponseDTO>.Failure(404, "Event not found");

        return Result<EventResponseDTO>.Success(_mapper.Map<EventResponseDTO>(eventEntity));
    }
}