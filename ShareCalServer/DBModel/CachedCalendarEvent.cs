using Microsoft.EntityFrameworkCore;

namespace ShareCalServer.DBModel;

public class CachedCalendarEvent : CalendarEventData
{
    public Guid Guid { get; set; }

    public DateTimeOffset Retrieved { get; set; }
    
    public Guid CalendarSourceGuid { get; set; }
    public virtual CalendarSource CalendarSource { get; set; }
}