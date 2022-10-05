using Microsoft.EntityFrameworkCore;
using ShareCalServer.Models;

namespace ShareCalServer.Services;

public interface ICalendarEventService
{
    Task<CalendarEvent?> GetEvent(Guid eventGuid);
    Task<CalendarEvent> CreateEvent(CreateEventModel model);
}

public class CalendarEventService : ICalendarEventService
{
    public CalendarEventService() { }
    
    public async Task<CalendarEvent?> GetEvent(Guid eventGuid)
    {
        await using var entities = new Entities();

        return await entities.CalendarEvents
            .SingleOrDefaultAsync(ce => ce.Guid == eventGuid);
    }

    public async Task<CalendarEvent> CreateEvent(CreateEventModel model)
    {
        await using var entities = new Entities();
        var newGuid = (await entities.CalendarEvents.AddAsync(
            new CalendarEvent()
            {
                EventStart = model.EventStart,
                EventEnd = model.EventEnd,
                Summary = model.Summary,
                Description = model.Description,
                Location = model.Location,
                DateCreated = DateTime.Now,
                LastModified = DateTime.Now
            }
            )).Entity.Guid;

        await entities.CalendarEventInclusions.AddRangeAsync(
            model.CalendarsIncludedIn
                .Select(guid => new CalendarEventInclusion()
                {
                    CalendarGuid = guid,
                    CalendarEventGuid = newGuid
                })
        );

        await entities.SaveChangesAsync();
        
        return await entities.CalendarEvents.SingleAsync(ce => ce.Guid == newGuid);
    }
}