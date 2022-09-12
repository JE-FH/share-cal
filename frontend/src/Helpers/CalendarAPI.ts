import { CalendarViewDTO, CreateCalendarDTO, CreateEventDTO, FullEventDTO } from "@/generated/DTO";

export default class CalendarAPI {
    private endpointPrefix: string = "/";
    
    constructor() {

    }

    public async GetCalendar(guid: string): Promise<CalendarViewDTO | null> {
        return await fetch(this.endpointPrefix + guid)
            .then((res) => {
                if (res.status == 404) {
                    return null;
                }
                return res.json()
            })
            .then((j) => CalendarViewDTO.FromObject(j));
    }

    public async CreateCalendar(dto: CreateCalendarDTO): Promise<CalendarViewDTO> {
        
    }

    public async GetEvent(guid: string): Promise<FullEventDTO | null> {
        return fetch(this.endpointPrefix + "events/" + guid)
            .then((res) => {
                if (res.status == 404) {
                    return null;
                }
                return res.json()
            })
            .then((j) => FullEventDTO.FromObject(j));
    }

    public async CreateEvent(dto: CreateEventDTO): Promise<FullEventDTO> {

    }
}