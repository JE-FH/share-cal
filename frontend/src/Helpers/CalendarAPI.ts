import { CalendarBriefDto, CalendarViewDTO, CreateCalendarDTO, CreateEventDTO, FullEventDTO } from "@/generated/DTO";

export interface ICalendarApi {
	GetCalendar(guid: string): Promise<CalendarViewDTO | null>;
	CreateCalendar(dto: CreateCalendarDTO): Promise<CalendarViewDTO>;
	GetEvent(guid: string): Promise<FullEventDTO | null>;
	CreateEvent(dto: CreateEventDTO): Promise<FullEventDTO>;
	GetCalendarList(): Promise<Array<CalendarBriefDto>>;
}

export class CalendarAPI implements ICalendarApi {
	private endpointPrefix = "https://localhost:7253/Calendar/";

	public async GetCalendar(guid: string): Promise<CalendarViewDTO | null> {
		return await fetch(this.endpointPrefix + "Get?id=" + guid)
			.then((res) => {
				if (res.status == 404) {
					return null;
				}
				return res.json()
			})
			.then((j) => CalendarViewDTO.FromObject(j));
	}

	public async CreateCalendar(dto: CreateCalendarDTO): Promise<CalendarViewDTO> {
		return await fetch(this.endpointPrefix + "Create", {
			method: 'POST',
			headers: {
				"Content-Type": "application/json"
			},
			body: dto.ToJson()
		})
			.then((res) => res.json)
			.then((j) => CalendarViewDTO.FromObject(j));
	}

	public async GetEvent(guid: string): Promise<FullEventDTO | null> {
		return fetch(this.endpointPrefix + "GetEvent/" + guid)
			.then((res) => {
				if (res.status == 404) {
					return null;
				}
				return res.json()
			})
			.then((j) => FullEventDTO.FromObject(j));
	}

	public async CreateEvent(dto: CreateEventDTO): Promise<FullEventDTO> {
		return await fetch(this.endpointPrefix + "CreateEvent/", {
			method: 'POST',
			headers: {
				"Content-Type": "application/json"
			},
			body: dto.ToJson()
		})
			.then((res) => res.json())
			.then((j) => FullEventDTO.FromObject(j));
	}

	public async GetCalendarList(): Promise<Array<CalendarBriefDto>> {
		return await fetch(this.endpointPrefix + "GetCalendarList")
			.then((res) => res.json())
			.then((j) => {
				console.log(j);
				if (!(j instanceof Array)) {
					throw new Error("Expected array");
				}
				return j.map((x) => CalendarBriefDto.FromObject(x));
			});
	}
}