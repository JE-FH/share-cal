//Day and month start at 1
export interface DateOnly {
	year: number;
	month: number;
	day: number;
}

export interface TimeOnly {
	hour: number;
	minute: number;
	seconds: number;
}

export interface IDateTimeHelper {
	FormatDateOnly(date: DateOnly): string;
	FormatTimeOnly(time: TimeOnly): string;
	ExtractDate(dateTime: Date): DateOnly;
	ExtractTime(dateTime: Date): TimeOnly;
	ToDate(date: DateOnly, time: TimeOnly): Date;
	readonly UsesAmPm: boolean;
	IsSameDate(a: DateOnly, b: DateOnly): boolean;
}

export class DateTimeHelper implements IDateTimeHelper {
	private _language: string;
	
	constructor() {
		this._language = navigator.language;
	}

	FormatDateOnly(date: DateOnly): string {
		//We add 1 to month because month starts at 1 in DateOnly while in the date api they start at 0
		const utcDate = Date.UTC(date.year, date.month - 1, date.day);
		
		const formatter = new Intl.DateTimeFormat(this._language, {timeZone: "UTC"});
		
		return formatter.format(utcDate);
	}

	FormatTimeOnly(time: TimeOnly): string {
		const utcTime = Date.UTC(1970, 0, 1, time.hour, time.minute, time.seconds);

		const formatter = new Intl.DateTimeFormat(this._language, {timeZone: "UTC", dateStyle: undefined, timeStyle: "short"});

		return formatter.format(utcTime)
	}

	ExtractDate(dateTime: Date): DateOnly {
		return {
			year: dateTime.getFullYear(),
			//We add 1 to month because month starts at 1 in DateOnly while in the date api they start at 0
			month: dateTime.getMonth() + 1,
			day: dateTime.getDate()
		}
	}

	ExtractTime(dateTime: Date): TimeOnly {
		return {
			hour: dateTime.getHours(),
			minute: dateTime.getMinutes(),
			seconds: dateTime.getSeconds()
		};
	}

	private ToISO8601(date: DateOnly, time: TimeOnly): string {
		return `${date.year.toString().padStart(4, "0")}-${date.month.toString().padStart(2, "0")}-${date.day.toString().padStart(2, "0")}`
		+ `T${time.hour.toString().padStart(2, '0')}:${time.minute.toString().padStart(2, "0")}:${time.seconds.toString().padStart(2, "0")}`;
	}

	ToDate(date: DateOnly, time: TimeOnly): Date {
		return new Date(this.ToISO8601(date, time));
	}

	IsSameDate(a: DateOnly, b: DateOnly): boolean {
		return a.year == b.year && a.month == b.month && a.day == b.day;
	}

	get UsesAmPm(): boolean {
		const hourCycle = Intl.DateTimeFormat(this._language, {hour: "numeric"}).resolvedOptions().hourCycle;
		return hourCycle == "h12" || hourCycle == "h11";
	}
}
