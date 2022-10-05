namespace ShareCal.DTO;

public class FullEventDTO
{
    public Guid Guid { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public List<Guid> CalendarsIncludedIn { get; set; }
}