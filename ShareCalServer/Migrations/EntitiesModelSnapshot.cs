﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShareCalServer.Models;

#nullable disable

namespace ShareCalServer.Migrations
{
    [DbContext(typeof(Entities))]
    partial class EntitiesModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("ShareCalServer.Models.CachedCalendarEvent", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CalendarSourceGuid")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("EventEnd")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("EventStart")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("Retrieved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Guid");

                    b.HasIndex("CalendarSourceGuid");

                    b.ToTable("CachedCalendarEvents");
                });

            modelBuilder.Entity("ShareCalServer.Models.Calendar", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Guid");

                    b.ToTable("Calendars");
                });

            modelBuilder.Entity("ShareCalServer.Models.CalendarEvent", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("EventEnd")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("EventStart")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("LastModified")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Guid");

                    b.ToTable("CalendarEvents");
                });

            modelBuilder.Entity("ShareCalServer.Models.CalendarEventInclusion", b =>
                {
                    b.Property<Guid>("CalendarGuid")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CalendarEventGuid")
                        .HasColumnType("TEXT");

                    b.HasKey("CalendarGuid", "CalendarEventGuid");

                    b.HasIndex("CalendarEventGuid");

                    b.ToTable("CalendarEventInclusions");
                });

            modelBuilder.Entity("ShareCalServer.Models.CalendarSource", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("CalendarSourceType")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("CalendarViewGuid")
                        .HasColumnType("TEXT");

                    b.Property<string>("ResourceString")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Guid");

                    b.HasIndex("CalendarViewGuid");

                    b.ToTable("CalendarSources");
                });

            modelBuilder.Entity("ShareCalServer.Models.CalendarView", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Guid");

                    b.ToTable("CalendarViews");
                });

            modelBuilder.Entity("ShareCalServer.Models.CachedCalendarEvent", b =>
                {
                    b.HasOne("ShareCalServer.Models.CalendarSource", "CalendarSource")
                        .WithMany("CachedCalendarEvents")
                        .HasForeignKey("CalendarSourceGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CalendarSource");
                });

            modelBuilder.Entity("ShareCalServer.Models.CalendarEventInclusion", b =>
                {
                    b.HasOne("ShareCalServer.Models.CalendarEvent", "CalendarEvent")
                        .WithMany("CalendarEventInclusions")
                        .HasForeignKey("CalendarEventGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShareCalServer.Models.Calendar", "Calendar")
                        .WithMany("CalendarEventInclusions")
                        .HasForeignKey("CalendarGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calendar");

                    b.Navigation("CalendarEvent");
                });

            modelBuilder.Entity("ShareCalServer.Models.CalendarSource", b =>
                {
                    b.HasOne("ShareCalServer.Models.CalendarView", "CalendarView")
                        .WithMany("Sources")
                        .HasForeignKey("CalendarViewGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CalendarView");
                });

            modelBuilder.Entity("ShareCalServer.Models.Calendar", b =>
                {
                    b.Navigation("CalendarEventInclusions");
                });

            modelBuilder.Entity("ShareCalServer.Models.CalendarEvent", b =>
                {
                    b.Navigation("CalendarEventInclusions");
                });

            modelBuilder.Entity("ShareCalServer.Models.CalendarSource", b =>
                {
                    b.Navigation("CachedCalendarEvents");
                });

            modelBuilder.Entity("ShareCalServer.Models.CalendarView", b =>
                {
                    b.Navigation("Sources");
                });
#pragma warning restore 612, 618
        }
    }
}
