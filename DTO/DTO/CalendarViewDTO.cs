namespace ShareCal.DTO;

public class CalendarViewDTO
{
    public Guid Guid;
    public string Name;
    public DateTime RangeStart;
    public DateTime RangeEnd;
    public List<EventBriefDTO> Events;
}