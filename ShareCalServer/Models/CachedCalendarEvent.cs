using Microsoft.EntityFrameworkCore;

namespace ShareCalServer.Models;

public class CachedCalendarEvent : CalendarEventData
{
    public Guid Guid { get; set; }

    public DateTime Retrieved { get; set; }
    
    public Guid CalendarSourceGuid { get; set; }
    public virtual CalendarSource CalendarSource { get; set; }
}