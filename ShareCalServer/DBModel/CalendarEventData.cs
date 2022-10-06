namespace ShareCalServer.DBModel;

public class CalendarEventData
{
    public DateTimeOffset EventStart { get; set; }
    public DateTimeOffset EventEnd { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public DateTimeOffset DateCreated { get; set; }
}