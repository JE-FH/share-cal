using Microsoft.EntityFrameworkCore;

namespace ShareCalServer.Models;

public class CalendarView
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public virtual ICollection<CalendarSource> Sources { get; set; }
}