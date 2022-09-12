using ShareCalServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareCal.DTO;
using ShareCalServer.Mappers;
using ShareCalServer.Services;

namespace ShareCalServer.Controllers;

[ApiController]
[Route("[controller]")]
public class CalendarController : Controller
{
    private readonly CalendarMapper _calendarMapper;
    private readonly ICalendarService _calendarService;
    private readonly ICreateCalendarMapper _createCalendarMapper;
    private readonly ICalendarEventService _calendarEventService;
    private readonly ICalendarEventMapper _calendarEventMapper;
    private readonly ICreateEventMapper _createEventMapper;

    public CalendarController(
        CalendarMapper calendarMapper, 
        ICalendarService calendarService, 
        ICreateCalendarMapper createCalendarMapper, 
        ICalendarEventService calendarEventService,
        ICalendarEventMapper calendarEventMapper, 
        ICreateEventMapper createEventMapper
        )
    {
        _calendarMapper = calendarMapper;
        _calendarService = calendarService;
        _createCalendarMapper = createCalendarMapper;
        _calendarEventService = calendarEventService;
        _calendarEventMapper = calendarEventMapper;
        _createEventMapper = createEventMapper;
    }
    
    [HttpGet(Name = "{id:guid}")]
    public async Task<ActionResult<ShareCal.DTO.CalendarViewDTO>> Get(Guid id)
    {
        var calendar = await _calendarService.GetCalendar(id);
        if (calendar == null)
        {
            return NotFound();
        }

        return Ok(_calendarMapper.CalendarToDto(calendar));
    }

    [HttpPost(Name = "")]
    public async Task<ActionResult<ShareCal.DTO.CalendarViewDTO>> Create([FromBody] CreateCalendarDTO dto)
    {
        var model = _createCalendarMapper.CreateCalendarToModel(dto);
        var calendar = await _calendarService.CreateCalendar(model);
        return Ok(_calendarMapper.CalendarToDto(calendar));
    }

    [HttpGet(Name = "events/{eventGuid:guid}")]
    public async Task<ActionResult<FullEventDTO>> GetEvent(Guid eventGuid)
    {
        var calendarEvent = await _calendarEventService.GetEvent(eventGuid);
        if (calendarEvent == null)
        {
            return NotFound();
        }

        return Ok(_calendarEventMapper.CalendarEventToFullDto(calendarEvent));
    }
    
    [HttpPost(Name = "events")]
    public async Task<ActionResult<FullEventDTO>> CreateEvent([FromBody] CreateEventDTO dto)
    {
        var model = _createEventMapper.CreateEventToModel(dto);
        var calendarEvent = await _calendarEventService.CreateEvent(model);
        return Ok(_calendarEventMapper.CalendarEventToFullDto(calendarEvent));
    }
}