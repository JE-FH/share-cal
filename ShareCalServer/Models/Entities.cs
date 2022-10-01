using Microsoft.EntityFrameworkCore;

namespace ShareCalServer.Models;

public class Entities : DbContext
{
    public string DbPath { get; }

    public DbSet<CalendarSource> CalendarSources { get; set; }
    public DbSet<CalendarView> CalendarViews { get; set; }
    public DbSet<Calendar> Calendars { get; set; }
    public DbSet<CalendarEvent> CalendarEvents { get; set; }
    public DbSet<CachedCalendarEvent> CachedCalendarEvents { get; set; }
    public DbSet<CalendarEventInclusion> CalendarEventInclusions { get; set; }

    public Entities()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "ShareCal.sqlite3");
        
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CalendarSource>(entity =>
        {
            entity.HasKey(cs => cs.Guid);
            entity
                .Property(cs => cs.Guid)
                .ValueGeneratedOnAdd();
            entity
                .HasOne(cs => cs.CalendarView)
                .WithMany(cv => cv.Sources)
                .HasForeignKey(cs => cs.CalendarViewGuid);
        });

        modelBuilder.Entity<CalendarView>(entity =>
        {
            entity.HasKey(cv => cv.Guid);
            entity
                .Property(cv => cv.Guid)
                .ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<CalendarEventInclusion>()
            .HasKey(cei => new { cei.CalendarGuid, cei.CalendarEventGuid });
        
        modelBuilder.Entity<Calendar>(entity =>
        {
            entity.HasKey(c => c.Guid);
            entity
                .Property(c => c.Guid)
                .ValueGeneratedOnAdd();
            entity
                .HasMany(c => c.CalendarEvents)
                .WithMany(ce => ce.Calendars)
                .UsingEntity<CalendarEventInclusion>(
                    j => j
                        .HasOne(cei => cei.CalendarEvent)
                        .WithMany(ce => ce.CalendarEventInclusions)
                        .HasForeignKey(cei => cei.CalendarEventGuid),
                    j => j
                        .HasOne(cei => cei.Calendar)
                        .WithMany(c => c.CalendarEventInclusions)
                        .HasForeignKey(cei => cei.CalendarGuid),
                    j => j
                        .HasKey(t => new { t.CalendarGuid, t.CalendarEventGuid })
                );

        });
        modelBuilder.Entity<CalendarEvent>(entity =>
        {
            entity.HasKey(ce => ce.Guid);
            entity.Property(ce => ce.Guid)
                .ValueGeneratedOnAdd();
            entity.Property(ce => ce.DateCreated)
                .ValueGeneratedOnAdd();
            entity.Property(ce => ce.LastModified)
                .ValueGeneratedOnUpdate();
        });

        modelBuilder.Entity<CachedCalendarEvent>(entity =>
        {
            entity.HasKey(cce => cce.Guid);
            entity.HasOne(cce => cce.CalendarSource)
                .WithMany(cs => cs.CachedCalendarEvents)
                .HasForeignKey(cce => cce.CalendarSourceGuid);
            entity.Property(cce => cce.Retrieved)
                .ValueGeneratedOnAdd();
        });
    }
}