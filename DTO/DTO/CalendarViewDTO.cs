namespace ShareCal.DTO;

public class CalendarViewDTO
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public DateTimeOffset RangeStart { get; set; }
    public DateTimeOffset RangeEnd { get; set; }
    public List<EventBriefDTO> Events { get; set; }
}