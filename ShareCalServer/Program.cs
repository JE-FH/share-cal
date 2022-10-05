using ShareCalServer.Mappers;
using ShareCalServer.Models;
using ShareCalServer.Services;

var builder = WebApplication.CreateBuilder(args);
        

// Add services to the container.

builder.Services.AddScoped<ICalendarEventMapper, CalendarCalendarEventMapper>();
builder.Services.AddScoped<ICalendarMapper, CalendarMapper>();
builder.Services.AddScoped<ICreateCalendarMapper, CreateCalendarMapper>();
builder.Services.AddScoped<ICalendarService, CalendarService>();
builder.Services.AddScoped<ICalendarEventService, CalendarEventService>();
builder.Services.AddScoped<ICreateEventMapper, CreateEventMapper>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var context = new Entities())
{
    var inclusions = context.CalendarEventInclusions.ToList();
    
}

app.Run();