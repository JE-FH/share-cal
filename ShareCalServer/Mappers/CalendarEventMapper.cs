using ShareCal.DTO;
using ShareCalServer.Models;

namespace ShareCalServer.Mappers;

public interface ICalendarEventMapper
{
    EventBriefDTO CalendarEventToDto(CalendarEvent calendarEvent);
    FullEventDTO CalendarEventToFullDto(CalendarEvent calendarEvent);
}

public class CalendarCalendarEventMapper : ICalendarEventMapper
{
    public CalendarCalendarEventMapper() { }

    public EventBriefDTO CalendarEventToDto(CalendarEvent calendarEvent)
    {
        return new EventBriefDTO()
        {
            Guid = calendarEvent.Guid,
            Start = calendarEvent.EventStart,
            End = calendarEvent.EventEnd,
            Summary = calendarEvent.Summary,
            Location = calendarEvent.Location,
            CalendarsIncludedIn = calendarEvent.CalendarEventInclusions
                .Select(cei => cei.CalendarGuid)
                .ToList()
        };
    }

    public FullEventDTO CalendarEventToFullDto(CalendarEvent calendarEvent)
    {
        return new FullEventDTO()
        {
            Guid = calendarEvent.Guid,
            Start = calendarEvent.EventStart,
            End = calendarEvent.EventEnd,
            Summary = calendarEvent.Summary,
            Description = calendarEvent.Description,
            Location = calendarEvent.Location,
            CalendarsIncludedIn = calendarEvent.CalendarEventInclusions
                .Select(cei => cei.CalendarGuid)
                .ToList()
        };
    }
}

