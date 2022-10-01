using Microsoft.EntityFrameworkCore;
using ShareCalServer.Models;

namespace ShareCalServer.Services;

public interface ICalendarService
{
    public Task<Calendar> CreateCalendar(CreateCalendarModel createCalendarModel);
    public Task<Calendar?> GetCalendar(Guid guid);
    public Task<IEnumerable<Calendar>> GetCalendars();
}

public class CalendarService : ICalendarService
{
    public CalendarService() { }

    public async Task<Calendar> CreateCalendar(CreateCalendarModel createCalendarModel)
    {
        await using var context = new Entities();
        var entry = await context.Calendars.AddAsync(new Calendar()
        {
            Guid = Guid.NewGuid(),
            Name = createCalendarModel.Name
        });

        await context.SaveChangesAsync();
        
        return entry.Entity;
    }

    public async Task<Calendar?> GetCalendar(Guid guid)
    {
        await using var context = new Entities();
        var calendar = await context.Calendars
            .Include(c => c.CalendarEvents)
            .Include(c => c.CalendarEventInclusions)
            .SingleOrDefaultAsync(calendar => calendar.Guid == guid);
        return calendar;
    }

    public async Task<IEnumerable<Calendar>> GetCalendars()
    {
        await using var context = new Entities();
        return await context.Calendars.ToListAsync();
    }
}