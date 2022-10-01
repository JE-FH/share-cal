//Day and month start at 1
export interface DateOnly {
	year: number;
	month: number;
	day: number;
}

export interface IDateHelper {
	FormatDateOnly(date: DateOnly): string;
}

export class DateHelper {
	private _language: string;
	
	constructor() {
		this._language = navigator.language
	}

	FormatDateOnly(date: DateOnly): string {
		//We add 1 to month because month starts at 1 in DateOnly while in the date api they start at 0
		const utcDate = Date.UTC(date.year, date.month - 1, date.day);
		
		const formatter = new Intl.DateTimeFormat(this._language, {timeZone: "UTC"});
		
		return formatter.format(utcDate);
	}
}
