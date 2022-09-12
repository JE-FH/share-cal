namespace ShareCalServer.Models;

public class CalendarEventInclusion
{
    public Guid CalendarGuid { get; set; }
    public Calendar Calendar { get; set; }
    
    public Guid CalendarEventGuid { get; set; }
    public CalendarEvent CalendarEvent { get; set; }
}