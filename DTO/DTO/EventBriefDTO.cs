namespace ShareCal.DTO;

public class EventBriefDTO
{
    public Guid Guid { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Summary { get; set; }
    public string Location { get; set; }
    public List<Guid> CalendarsIncludedIn { get; set; }
}