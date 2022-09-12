// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using ShareCal.DTO;


Transpiler transpiler = new Transpiler();

transpiler
    .AddDto(typeof(CalendarEventDTO))
    .AddDto(typeof(CalendarViewDTO))
    .AddDto(typeof(CreateCalendarDTO))
    .AddDto(typeof(CreateEventDTO))
    .AddDto(typeof(EventBriefDTO))
    .AddDto(typeof(FullEventDTO));

File.WriteAllText("../frontend/src/generated/DTO.ts", transpiler.TranspileToTypescript());

//Console.WriteLine(transpiler.TranspileToTypescript());
