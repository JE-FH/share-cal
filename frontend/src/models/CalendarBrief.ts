import { CalendarBriefDto } from "@/generated/DTO";

export interface CalendarBrief {
    Guid: string;
    Name: string;
}

export class CalendarBriefMapper {
    public static CalendarBriefDtoToModel(dto: CalendarBriefDto) {
        return {
            Guid: dto.Guid,
            Name: dto.Name
        };
    }
}