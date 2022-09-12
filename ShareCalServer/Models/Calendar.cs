using Microsoft.EntityFrameworkCore;

namespace ShareCalServer.Models;

public class Calendar
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public virtual ICollection<CalendarEvent> CalendarEvents { get; set; }
    public virtual ICollection<CalendarEventInclusion> CalendarEventInclusions { get; set; }
}