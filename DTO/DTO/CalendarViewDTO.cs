namespace ShareCal.DTO;

public class CalendarViewDTO
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public DateTime RangeStart { get; set; }
    public DateTime RangeEnd { get; set; }
    public List<EventBriefDTO> Events { get; set; }
}