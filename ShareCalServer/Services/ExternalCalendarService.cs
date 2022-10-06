using Microsoft.EntityFrameworkCore;
using ShareCalServer.DBModel;
using ShareCalServer.Model;

namespace ShareCalServer.Services;

public interface IExternalCalendarService
{
    Task<CalendarSource> AddICalCalendarSource(CreateICalCalendarSourceModel model);
    Task UpdateCalendarSourceCache(Guid calendarSourceGuid);
    Task ClearCalendarSourceCache(Guid calendarSourceGuid);
    Task RemoveCalendarSource(Guid calendarSourceGuid);
}

public class ExternalCalendarService : IExternalCalendarService
{
    public async Task<CalendarSource> AddICalCalendarSource(CreateICalCalendarSourceModel model)
    {
        await using var entities = new Entities();
        var result = await entities.CalendarSources.AddAsync(new CalendarSource()
        {
            CalendarSourceType = CalendarSourceType.ICal,
            ResourceString = model.ResourcePath,
            CalendarViewGuid = model.CalendarViewGuid
        });

        var newGuid = result.Entity.Guid;

        await entities.SaveChangesAsync();

        return await entities.CalendarSources.SingleAsync((cs) => newGuid == cs.Guid);
;   }

    public Task UpdateCalendarSourceCache(Guid calendarSourceGuid)
    {
        throw new NotImplementedException();
    }

    public Task ClearCalendarSourceCache(Guid calendarSourceGuid)
    {
        throw new NotImplementedException();
    }

    public Task RemoveCalendarSource(Guid calendarSourceGuid)
    {
        throw new NotImplementedException();
    }
}
