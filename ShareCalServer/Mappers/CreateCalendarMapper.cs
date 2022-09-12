using ShareCal.DTO;
using ShareCalServer.Models;

namespace ShareCalServer.Mappers;

public interface ICreateCalendarMapper
{
    public CreateCalendarModel CreateCalendarToModel(CreateCalendarDTO dto);
}

public class CreateCalendarMapper : ICreateCalendarMapper
{
    public CreateCalendarMapper() { }


    public CreateCalendarModel CreateCalendarToModel(CreateCalendarDTO dto)
    {
        return new CreateCalendarModel()
        {
            Name = dto.Name
        };
    }
}