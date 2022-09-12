namespace ShareCal.DTO;

public class FullEventDTO
{
    public Guid Guid;
    public DateTime Start;
    public DateTime End;
    public string Summary;
    public string Description;
    public string Location;
    public List<Guid> CalendarsIncludedIn;
}