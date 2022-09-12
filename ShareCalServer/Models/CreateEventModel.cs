namespace ShareCalServer.Models;

public class CreateEventModel
{
    public DateTime EventStart { get; set; }
    public DateTime EventEnd { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public List<Guid> CalendarsIncludedIn { get; set; }
}