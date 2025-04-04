﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using violet.backend.Infrastructures;

#nullable disable

namespace violet.backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250404095204_RemoveExtraProperties")]
    partial class RemoveExtraProperties
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("violet.backend.Entities.Event", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<Guid>("AggregateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DomainEventType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("EventData")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2025, 4, 4, 13, 22, 3, 813, DateTimeKind.Local).AddTicks(9133));

                    b.Property<int>("Version")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("violet.backend.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("InitialSetup")
                        .HasColumnType("bit");

                    b.Property<Guid>("PartnerUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .HasMaxLength(200)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Username")
                        .HasMaxLength(200)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("violet.backend.Entities.FemaleUser", b =>
                {
                    b.HasBaseType("violet.backend.Entities.User");

                    b.ToTable("FemaleUsers", (string)null);
                });

            modelBuilder.Entity("violet.backend.Entities.MaleUser", b =>
                {
                    b.HasBaseType("violet.backend.Entities.User");

                    b.ToTable("MaleUsers", (string)null);
                });

            modelBuilder.Entity("violet.backend.Entities.User", b =>
                {
                    b.OwnsOne("violet.backend.Entities.TelegramInfo", "TelegramInfo", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<bool>("AddedToAttachmentMenu")
                                .HasColumnType("bit");

                            b1.Property<bool>("AllowsWriteToPm")
                                .HasColumnType("bit");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(200)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(200)");

                            b1.Property<bool>("IsBot")
                                .HasColumnType("bit");

                            b1.Property<bool>("IsPremium")
                                .HasColumnType("bit");

                            b1.Property<string>("LanguageCode")
                                .HasMaxLength(50)
                                .IsUnicode(false)
                                .HasColumnType("varchar(50)");

                            b1.Property<string>("LastName")
                                .HasMaxLength(200)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(200)");

                            b1.Property<string>("PhotoUrl")
                                .HasMaxLength(500)
                                .IsUnicode(false)
                                .HasColumnType("varchar(500)");

                            b1.Property<long>("TelegramId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Username")
                                .HasMaxLength(200)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(200)");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("TelegramInfo");
                });

            modelBuilder.Entity("violet.backend.Entities.FemaleUser", b =>
                {
                    b.HasOne("violet.backend.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("violet.backend.Entities.FemaleUser", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("violet.backend.Entities.FemaleProfile", "FemaleProfile", b1 =>
                        {
                            b1.Property<Guid>("FemaleUserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("BirthYear")
                                .HasColumnType("int");

                            b1.Property<int>("BleedingDuration")
                                .HasColumnType("int");

                            b1.Property<int>("Height")
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .HasMaxLength(200)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(200)");

                            b1.Property<int>("PeriodCycleDuration")
                                .HasColumnType("int");

                            b1.Property<decimal>("Weigh")
                                .HasColumnType("DECIMAL(5,2)");

                            b1.HasKey("FemaleUserId");

                            b1.ToTable("FemaleUsers");

                            b1.WithOwner()
                                .HasForeignKey("FemaleUserId");
                        });

                    b.OwnsMany("violet.backend.Entities.Period", "Periods", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("CreateDate")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime?>("EndDayOfBleeding")
                                .HasColumnType("datetime2");

                            b1.Property<Guid?>("FemaleUserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("StartDayOfPeriod")
                                .HasColumnType("datetime2");

                            b1.HasKey("Id");

                            b1.HasIndex("FemaleUserId");

                            b1.ToTable("Periods", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("FemaleUserId");
                        });

                    b.Navigation("FemaleProfile");

                    b.Navigation("Periods");
                });

            modelBuilder.Entity("violet.backend.Entities.MaleUser", b =>
                {
                    b.HasOne("violet.backend.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("violet.backend.Entities.MaleUser", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("violet.backend.Entities.MaleProfile", "MaleProfile", b1 =>
                        {
                            b1.Property<Guid>("MaleUserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("BirthYear")
                                .HasColumnType("int");

                            b1.Property<int>("Height")
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .HasMaxLength(200)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(200)");

                            b1.Property<decimal>("Weigh")
                                .HasColumnType("DECIMAL(5,2)");

                            b1.HasKey("MaleUserId");

                            b1.ToTable("MaleUsers");

                            b1.WithOwner()
                                .HasForeignKey("MaleUserId");
                        });

                    b.Navigation("MaleProfile");
                });
#pragma warning restore 612, 618
        }
    }
}
