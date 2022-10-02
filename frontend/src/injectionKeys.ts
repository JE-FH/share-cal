import { InjectionKey } from "vue";
import { ICalendarApi } from "./Helpers/CalendarAPI";
import { IDateTimeHelper } from "./Helpers/DateTimeHelper";

export const calendarApiSymbol: InjectionKey<ICalendarApi> = Symbol("CalendarApi");
export const dateTimeHelperSymbol: InjectionKey<IDateTimeHelper> = Symbol("DateHelper");