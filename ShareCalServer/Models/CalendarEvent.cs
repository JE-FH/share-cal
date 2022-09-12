using Microsoft.EntityFrameworkCore;

namespace ShareCalServer.Models;

public class CalendarEvent : CalendarEventData
{
    public Guid Guid { get; set; }
    public virtual ICollection<Calendar> Calendars { get; set; }
    public virtual ICollection<CalendarEventInclusion> CalendarEventInclusions { get; set; }

}