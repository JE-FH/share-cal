using Microsoft.AspNetCore.Mvc;
using ShareCal.DTO;
using ShareCalServer.Mappers;
using ShareCalServer.Services;

namespace ShareCalServer.Controllers;

public class CalendarController : Controller
{
    private readonly ICalendarMapper _calendarMapper;
    private readonly ICalendarService _calendarService;
    private readonly ICreateCalendarMapper _createCalendarMapper;
    private readonly ICalendarEventService _calendarEventService;
    private readonly ICalendarEventMapper _calendarEventMapper;
    private readonly ICreateEventMapper _createEventMapper;

    public CalendarController(
        ICalendarMapper calendarMapper, 
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
    
    [HttpGet]
    public async Task<ActionResult<List<CalendarBriefDto>>> GetCalendarList()
    {
        return (await _calendarService.GetCalendars())
            .Select(calendar => _calendarMapper.CalendarToBriefDto(calendar))
            .ToList();
    }
    
    [HttpGet]
    public async Task<ActionResult<ShareCal.DTO.CalendarViewDTO>> Get(Guid id)
    {
        var calendar = await _calendarService.GetCalendar(id);
        if (calendar == null)
        {
            return NotFound();
        }

        return Ok(_calendarMapper.CalendarToDto(calendar));
    }

    [HttpPost]
    public async Task<ActionResult<ShareCal.DTO.CalendarViewDTO>> Create([FromBody] CreateCalendarDTO dto)
    {
        var model = _createCalendarMapper.CreateCalendarToModel(dto);
        var calendar = await _calendarService.CreateCalendar(model);
        return Ok(_calendarMapper.CalendarToDto(calendar));
    }

    [HttpGet]
    public async Task<ActionResult<FullEventDTO>> GetEvent(Guid eventGuid)
    {
        var calendarEvent = await _calendarEventService.GetEvent(eventGuid);
        if (calendarEvent == null)
        {
            return NotFound();
        }

        return Ok(_calendarEventMapper.CalendarEventToFullDto(calendarEvent));
    }
    
    [HttpPost]
    public async Task<ActionResult<FullEventDTO>> CreateEvent([FromBody] CreateEventDTO dto)
    {
        var model = _createEventMapper.CreateEventToModel(dto);
        var calendarEvent = await _calendarEventService.CreateEvent(model);
        return Ok(_calendarEventMapper.CalendarEventToFullDto(calendarEvent));
    }
}