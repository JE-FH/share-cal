namespace ShareCal.DTO;

public class EventBriefDTO
{
    public Guid Guid;
    public DateTime Start;
    public DateTime End;
    public string Summary;
    public string Location;
    public List<Guid> CalendarsIncludedIn;
}