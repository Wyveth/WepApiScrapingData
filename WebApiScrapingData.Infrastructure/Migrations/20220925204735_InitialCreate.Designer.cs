﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiScrapingData.Infrastructure.Data;

#nullable disable

namespace WebApiScrapingData.Infrastructure.Migrations
{
    [DbContext(typeof(ScrapingContext))]
    [Migration("20220925204735_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApiScrapingData.Domain.Class.DataInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModification")
                        .HasColumnType("datetime2");

                    b.Property<string>("DescriptionTalent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescriptionVx")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescriptionVy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Evolutions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NextUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Talent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Types")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCreation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserModification")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Weakness")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Weight")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhenEvolution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("versionModification")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DataInfos");
                });

            modelBuilder.Entity("WebApiScrapingData.Domain.Class.Pokemon", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModification")
                        .HasColumnType("datetime2");

                    b.Property<int>("Generation")
                        .HasColumnType("int");

                    b.Property<long>("Id_CN")
                        .HasColumnType("bigint");

                    b.Property<long>("Id_CO")
                        .HasColumnType("bigint");

                    b.Property<long>("Id_DE")
                        .HasColumnType("bigint");

                    b.Property<long>("Id_EN")
                        .HasColumnType("bigint");

                    b.Property<long>("Id_ES")
                        .HasColumnType("bigint");

                    b.Property<long>("Id_FR")
                        .HasColumnType("bigint");

                    b.Property<long>("Id_IT")
                        .HasColumnType("bigint");

                    b.Property<long>("Id_JP")
                        .HasColumnType("bigint");

                    b.Property<long>("Id_RU")
                        .HasColumnType("bigint");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatAttaque")
                        .HasColumnType("int");

                    b.Property<int>("StatAttaqueSpe")
                        .HasColumnType("int");

                    b.Property<int>("StatDefense")
                        .HasColumnType("int");

                    b.Property<int>("StatDefenseSpe")
                        .HasColumnType("int");

                    b.Property<int>("StatPv")
                        .HasColumnType("int");

                    b.Property<int>("StatTotal")
                        .HasColumnType("int");

                    b.Property<int>("StatVitesse")
                        .HasColumnType("int");

                    b.Property<string>("TypeEvolution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlImg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlSprite")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCreation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserModification")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("versionModification")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id_CN");

                    b.HasIndex("Id_CO");

                    b.HasIndex("Id_DE");

                    b.HasIndex("Id_EN");

                    b.HasIndex("Id_ES");

                    b.HasIndex("Id_FR");

                    b.HasIndex("Id_IT");

                    b.HasIndex("Id_JP");

                    b.HasIndex("Id_RU");

                    b.ToTable("Pokemons");
                });

            modelBuilder.Entity("WebApiScrapingData.Domain.Class.TypePok", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModification")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImgColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InfoColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name_CN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name_CO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name_DE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name_EN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name_ES")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name_FR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name_IT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name_JP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name_RU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlAutoHome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlFondGo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlIconHome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlMiniGo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlMiniHome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCreation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserModification")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("versionModification")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TypesPok");
                });

            modelBuilder.Entity("WebApiScrapingData.Domain.Class.Pokemon", b =>
                {
                    b.HasOne("WebApiScrapingData.Domain.Class.DataInfo", "CN")
                        .WithMany()
                        .HasForeignKey("Id_CN")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WebApiScrapingData.Domain.Class.DataInfo", "CO")
                        .WithMany()
                        .HasForeignKey("Id_CO")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WebApiScrapingData.Domain.Class.DataInfo", "DE")
                        .WithMany()
                        .HasForeignKey("Id_DE")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WebApiScrapingData.Domain.Class.DataInfo", "EN")
                        .WithMany()
                        .HasForeignKey("Id_EN")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WebApiScrapingData.Domain.Class.DataInfo", "ES")
                        .WithMany()
                        .HasForeignKey("Id_ES")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WebApiScrapingData.Domain.Class.DataInfo", "FR")
                        .WithMany()
                        .HasForeignKey("Id_FR")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WebApiScrapingData.Domain.Class.DataInfo", "IT")
                        .WithMany()
                        .HasForeignKey("Id_IT")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WebApiScrapingData.Domain.Class.DataInfo", "JP")
                        .WithMany()
                        .HasForeignKey("Id_JP")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WebApiScrapingData.Domain.Class.DataInfo", "RU")
                        .WithMany()
                        .HasForeignKey("Id_RU")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CN");

                    b.Navigation("CO");

                    b.Navigation("DE");

                    b.Navigation("EN");

                    b.Navigation("ES");

                    b.Navigation("FR");

                    b.Navigation("IT");

                    b.Navigation("JP");

                    b.Navigation("RU");
                });
#pragma warning restore 612, 618
        }
    }
}
