namespace ShareCalServer.Models;

public class CalendarEventData
{
    public DateTime EventStart { get; set; }
    public DateTime EventEnd { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime LastModified { get; set; }
    public DateTime DateCreated { get; set; }
}