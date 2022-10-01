using ShareCal.DTO;
using ShareCalServer.Models;

namespace ShareCalServer.Mappers;

public interface ICalendarMapper
{
    CalendarViewDTO CalendarToDto(Calendar calendar);
    CalendarBriefDto CalendarToBriefDto(Calendar calendar);
}

public class CalendarMapper : ICalendarMapper
{
    private readonly ICalendarEventMapper _calendarEventMapper;

    public CalendarMapper(ICalendarEventMapper calendarEventMapper)
    {
        _calendarEventMapper = calendarEventMapper;
    }

    public CalendarViewDTO CalendarToDto(Calendar calendar)
    {
        var events = calendar.CalendarEvents
            .Select(
                calendarEvent => _calendarEventMapper.CalendarEventToDto(calendarEvent)
                ).ToList();

        return new CalendarViewDTO()
        {
            Guid = calendar.Guid,
            Name = calendar.Name,
            RangeStart = DateTime.MinValue,
            RangeEnd = DateTime.MaxValue,
            Events = events
        };
    }

    public CalendarBriefDto CalendarToBriefDto(Calendar calendar)
    {
        return new CalendarBriefDto()
        {
            Guid = calendar.Guid,
            Name = calendar.Name
        };
    }
}
