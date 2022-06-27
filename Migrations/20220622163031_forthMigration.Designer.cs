﻿// <auto-generated />
using System;
using KaiBeltCsharp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KaiBeltCsharp.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20220622163031_forthMigration")]
    partial class forthMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("KaiBeltCsharp.Models.Meet", b =>
                {
                    b.Property<int>("MeetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateandTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("DurationStyle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("MeetId");

                    b.HasIndex("UserId");

                    b.ToTable("Meets");
                });

            modelBuilder.Entity("KaiBeltCsharp.Models.Party", b =>
                {
                    b.Property<int>("GuestListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("MeetId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("GuestListId");

                    b.HasIndex("MeetId");

                    b.HasIndex("UserId");

                    b.ToTable("Parties");
                });

            modelBuilder.Entity("KaiBeltCsharp.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("KaiBeltCsharp.Models.Meet", b =>
                {
                    b.HasOne("KaiBeltCsharp.Models.User", "Creator")
                        .WithMany("Planned")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("KaiBeltCsharp.Models.Party", b =>
                {
                    b.HasOne("KaiBeltCsharp.Models.Meet", "Meet")
                        .WithMany("Guests")
                        .HasForeignKey("MeetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KaiBeltCsharp.Models.User", "Guest")
                        .WithMany("Attending")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guest");

                    b.Navigation("Meet");
                });

            modelBuilder.Entity("KaiBeltCsharp.Models.Meet", b =>
                {
                    b.Navigation("Guests");
                });

            modelBuilder.Entity("KaiBeltCsharp.Models.User", b =>
                {
                    b.Navigation("Attending");

                    b.Navigation("Planned");
                });
#pragma warning restore 612, 618
        }
    }
}
