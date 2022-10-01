import { InjectionKey } from "vue";
import { ICalendarApi } from "./Helpers/CalendarAPI";
import { IDateHelper } from "./Helpers/DateHelper";

export const calendarApiSymbol: InjectionKey<ICalendarApi> = Symbol("CalendarApi");
export const dateHelperSymbol: InjectionKey<IDateHelper> = Symbol("DateHelper");