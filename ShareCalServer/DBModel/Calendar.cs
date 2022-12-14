using Microsoft.EntityFrameworkCore;

namespace ShareCalServer.DBModel;

public class Calendar
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public ICollection<CalendarEvent> CalendarEvents { get; set; }
    public List<CalendarEventInclusion> CalendarEventInclusions { get; set; }
}