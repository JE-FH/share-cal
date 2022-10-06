namespace ShareCalServer.Model;

public class CreateEventModel
{
    public DateTimeOffset EventStart { get; set; }
    public DateTimeOffset EventEnd { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public List<Guid> CalendarsIncludedIn { get; set; }
}