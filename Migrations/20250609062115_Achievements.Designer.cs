﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using exomine.Data;

#nullable disable

namespace exomine.Migrations
{
    [DbContext(typeof(MineContext))]
    [Migration("20250609062115_Achievements")]
    partial class Achievements
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("exomine.Data.Models.Achievement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("GridType")
                        .HasColumnType("int");

                    b.Property<int?>("MaxTimeSeconds")
                        .HasColumnType("int");

                    b.Property<int?>("MinDifficulty")
                        .HasColumnType("int");

                    b.Property<int?>("MinSize")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Achievement");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MinSize = 5,
                            Name = "Win any game at least size 5"
                        },
                        new
                        {
                            Id = 2,
                            MinSize = 10,
                            Name = "Win any game at least size 10"
                        },
                        new
                        {
                            Id = 3,
                            GridType = 0,
                            MinSize = 5,
                            Name = "Win a hexagon game at size 5"
                        },
                        new
                        {
                            Id = 4,
                            GridType = 0,
                            MinSize = 10,
                            Name = "Win a hexagon game at size 10"
                        },
                        new
                        {
                            Id = 5,
                            GridType = 0,
                            MinDifficulty = 20,
                            Name = "Win a hexagon game at difficulty at least 20"
                        },
                        new
                        {
                            Id = 6,
                            GridType = 0,
                            MaxTimeSeconds = 30,
                            MinDifficulty = 20,
                            Name = "Win a hexagon game at difficulty at least 20 under 30 seconds"
                        },
                        new
                        {
                            Id = 7,
                            GridType = 1,
                            MinSize = 5,
                            Name = "Win a square game at size 5"
                        },
                        new
                        {
                            Id = 8,
                            GridType = 1,
                            MinSize = 10,
                            Name = "Win a square game at size 10"
                        },
                        new
                        {
                            Id = 9,
                            GridType = 1,
                            MinDifficulty = 20,
                            Name = "Win a square game at difficulty at least 20"
                        },
                        new
                        {
                            Id = 10,
                            GridType = 1,
                            MaxTimeSeconds = 30,
                            MinDifficulty = 20,
                            Name = "Win a square game at difficulty at least 20 under 30 seconds"
                        },
                        new
                        {
                            Id = 11,
                            GridType = 2,
                            MinSize = 5,
                            Name = "Win a triangle game at size 5"
                        },
                        new
                        {
                            Id = 12,
                            GridType = 2,
                            MinSize = 10,
                            Name = "Win a triangle game at size 10"
                        },
                        new
                        {
                            Id = 13,
                            GridType = 2,
                            MinDifficulty = 20,
                            Name = "Win a triangle game at difficulty at least 20"
                        },
                        new
                        {
                            Id = 14,
                            GridType = 2,
                            MaxTimeSeconds = 30,
                            MinDifficulty = 20,
                            Name = "Win a triangle game at difficulty at least 20 under 30 seconds"
                        },
                        new
                        {
                            Id = 15,
                            GridType = 3,
                            MinSize = 5,
                            Name = "Win a squareTriHex game at size 5"
                        },
                        new
                        {
                            Id = 16,
                            GridType = 3,
                            MinSize = 10,
                            Name = "Win a squareTriHex game at size 10"
                        },
                        new
                        {
                            Id = 17,
                            GridType = 3,
                            MinDifficulty = 20,
                            Name = "Win a squareTriHex game at difficulty at least 20"
                        },
                        new
                        {
                            Id = 18,
                            GridType = 3,
                            MaxTimeSeconds = 30,
                            MinDifficulty = 20,
                            Name = "Win a squareTriHex game at difficulty at least 20 under 30 seconds"
                        });
                });

            modelBuilder.Entity("exomine.Data.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Bombs")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("Difficulty")
                        .HasColumnType("int");

                    b.Property<string>("Known")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Revealed")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("exomine.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PasswordHash = "$2a$11$WkApcDx7b7xYmPEc77bwKuqClTsVbokRM1huCojjW40k8SKmqvfJy",
                            Role = "Admin",
                            Username = "core"
                        });
                });

            modelBuilder.Entity("exomine.Data.Models.UserAchievement", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("AchievementId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "AchievementId");

                    b.HasIndex("AchievementId");

                    b.ToTable("UserAchievement");
                });

            modelBuilder.Entity("exomine.Data.Models.UserGame", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.Property<bool>("Win")
                        .HasColumnType("bit");

                    b.HasKey("UserId", "GameId");

                    b.HasIndex("GameId");

                    b.ToTable("UserGames");
                });

            modelBuilder.Entity("exomine.Data.Models.UserAchievement", b =>
                {
                    b.HasOne("exomine.Data.Models.Achievement", "Achievement")
                        .WithMany()
                        .HasForeignKey("AchievementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("exomine.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Achievement");

                    b.Navigation("User");
                });

            modelBuilder.Entity("exomine.Data.Models.UserGame", b =>
                {
                    b.HasOne("exomine.Data.Models.Game", "Game")
                        .WithMany("PlayedBy")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("exomine.Data.Models.User", "User")
                        .WithMany("GamesPlayed")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("exomine.Data.Models.Game", b =>
                {
                    b.Navigation("PlayedBy");
                });

            modelBuilder.Entity("exomine.Data.Models.User", b =>
                {
                    b.Navigation("GamesPlayed");
                });
#pragma warning restore 612, 618
        }
    }
}
