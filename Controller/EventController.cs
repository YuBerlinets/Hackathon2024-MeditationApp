using meditationApp.DTO.events;
using meditationApp.Repositories.Abscrations;
using meditationApp.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace meditationApp.Controller;

public class EventController : BaseApiController
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<ActionResult<List<EventResponseDTO>>> GetEvents()
    {
        var events = await _eventService.GetAllEventsAsync();
        if (events.StatusCode != 200)
            return StatusCode(events.StatusCode, events.ErrorMessage);
        return Ok(events.Data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EventResponseDTO>> GetEvent(int id)
    {
        var eventResponse = await _eventService.GetEventByIdAsync(id);

        if (eventResponse.StatusCode != 200)
            return StatusCode(eventResponse.StatusCode, eventResponse.ErrorMessage);

        return Ok(eventResponse.Data);
    }

    [HttpPost]
    public async Task<ActionResult> CreateEvent([FromBody] CreateEventDTO createEventDTO)
    {
        var response = await _eventService.AddEventAsync(createEventDTO);
        if (response.StatusCode != 200)
            return StatusCode(response.StatusCode, response.ErrorMessage);
        return Ok(response.Data);
    }
}