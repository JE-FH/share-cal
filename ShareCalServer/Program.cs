using ShareCalServer.Mappers;
using ShareCalServer.Services;

var builder = WebApplication.CreateBuilder(args);
        

// Add services to the container.

builder.Services.AddScoped<ICalendarEventMapper, CalendarCalendarEventMapper>();
builder.Services.AddScoped<ICalendarMapper, CalendarMapper>();
builder.Services.AddScoped<ICreateCalendarMapper, CreateCalendarMapper>();
builder.Services.AddScoped<ICalendarService, CalendarService>();
builder.Services.AddScoped<ICalendarEventService, CalendarEventService>();
builder.Services.AddScoped<ICreateEventMapper, CreateEventMapper>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();