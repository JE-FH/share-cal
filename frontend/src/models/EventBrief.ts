import { EventBriefDTO } from "@/generated/DTO";

export interface EventBrief {
    Guid: string;
    Start: Date;
    End: Date;
    Summary: string;
    Location: string;
    CalendarsIncludedIn: Array<string>;
}

export class EventBriefMapper {
    static EventBriefDtoToModel(dto: EventBriefDTO): EventBrief {
        return {
            Guid: dto.Guid,
            Start: dto.Start,
            End: dto.End,
            Summary: dto.Summary,
            Location: dto.Location,
            CalendarsIncludedIn: dto.CalendarsIncludedIn
        };
    }
}