import { CalendarViewDTO } from "@/generated/DTO";
import { EventBrief, EventBriefMapper } from "./EventBrief";

export interface CalendarView {
	Guid: string;
	Name: string;
	RangeStart: Date;
	RangeEnd: Date;
	Events: Array<EventBrief>;
}

export class CalendarViewMapper {
	static CalendarViewDtoToModel(dto: CalendarViewDTO): CalendarView {
		return {
			Guid: dto.Guid,
			Name: dto.Name,
			RangeStart: dto.RangeStart,
			RangeEnd: dto.RangeEnd,
			Events: dto.Events.map((eventDto) => 
				EventBriefMapper.EventBriefDtoToModel(eventDto)
			)
		};
	}
}