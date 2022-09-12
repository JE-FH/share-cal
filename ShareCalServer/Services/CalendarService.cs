using Microsoft.EntityFrameworkCore;
using ShareCalServer.Models;

namespace ShareCalServer.Services;

public interface ICalendarService
{
    public Task<Calendar> CreateCalendar(CreateCalendarModel createCalendarModel);
    public Task<Calendar?> GetCalendar(Guid guid);
}

public class CalendarService : ICalendarService
{
    public CalendarService() { }

    public async Task<Calendar> CreateCalendar(CreateCalendarModel createCalendarModel)
    {
        await using var context = new Entities();
        
        var entry = await context.Calendars.AddAsync(new Calendar() {
            Guid = Guid.NewGuid(),
            Name = createCalendarModel.Name
        });

        return entry.Entity;
    }

    public async Task<Calendar?> GetCalendar(Guid guid)
    {
        await using var context = new Entities();
        var calendar = await context.Calendars
            .SingleOrDefaultAsync(calendar => calendar.Guid == guid);
        return calendar;
    }
}