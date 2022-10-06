namespace ShareCalServer.DBModel;

public class CalendarSource
{
    public Guid Guid { get; set; }
    public CalendarSourceType CalendarSourceType { get; set; }
    public string ResourceString { get; set; }
    public virtual ICollection<CachedCalendarEvent> CachedCalendarEvents { get; set; }
    public Guid CalendarViewGuid { get; set; }
    public virtual CalendarView CalendarView { get; set; }
}

public enum CalendarSourceType
{
    ShareCal,
    ICal,
    CalDAV
}