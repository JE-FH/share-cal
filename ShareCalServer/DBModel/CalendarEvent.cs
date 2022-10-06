using Microsoft.EntityFrameworkCore;

namespace ShareCalServer.DBModel;

public class CalendarEvent : CalendarEventData
{
    public Guid Guid { get; set; }
    public ICollection<Calendar> Calendars { get; set; }
    public List<CalendarEventInclusion> CalendarEventInclusions { get; set; }

}