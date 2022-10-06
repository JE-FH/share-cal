using ShareCal.DTO;
using ShareCalServer.Model;

namespace ShareCalServer.Mappers;

public interface ICreateEventMapper
{
    CreateEventModel CreateEventToModel(CreateEventDTO dto);
}

public class CreateEventMapper : ICreateEventMapper
{
    public CreateEventModel CreateEventToModel(CreateEventDTO dto)
    {
        return new CreateEventModel()
        {
            EventStart = dto.EventStart,
            EventEnd = dto.EventEnd,
            Summary = dto.Summary,
            Description = dto.Description,
            Location = dto.Location,
            CalendarsIncludedIn = dto.CalendarsIncludedIn
                .ToList()
        };
    }

}