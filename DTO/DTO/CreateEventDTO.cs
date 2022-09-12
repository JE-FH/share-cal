namespace ShareCal.DTO;

public class CreateEventDTO
{
    public DateTime EventStart;
    public DateTime EventEnd;
    public string Summary;
    public string Description;
    public string Location;
    public List<Guid> CalendarsIncludedIn;
}