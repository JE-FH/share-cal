using ShareCalServer.DBModel;

namespace ShareCalServer.Model;

public class CreateICalCalendarSourceModel
{
    public string ResourcePath { get; set; }
    public Guid CalendarViewGuid { get; set; }

}