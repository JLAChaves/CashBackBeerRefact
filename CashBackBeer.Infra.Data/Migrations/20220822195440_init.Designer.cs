﻿// <auto-generated />
using System;
using CashbackBeer.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CashBackBeer.Infra.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220822195440_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CashBackBeer.Domain.Entities.Beer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreateInUTC")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Beers");
                });

            modelBuilder.Entity("CashbackBeer.Domain.Entities.FinalSale", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreateInUTC")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("FinalSales");
                });

            modelBuilder.Entity("CashbackBeer.Domain.Entities.PartialSale", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<Guid>("BeerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreateInUTC")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("FinalSaleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BeerId");

                    b.HasIndex("FinalSaleId");

                    b.ToTable("PartialSales");
                });

            modelBuilder.Entity("CashBackBeer.Domain.Entities.Beer", b =>
                {
                    b.OwnsOne("CashBackBeer.Domain.Entities.CashBack", "CashBack", b1 =>
                        {
                            b1.Property<Guid>("BeerId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Day")
                                .HasColumnType("int");

                            b1.Property<decimal>("Percentage")
                                .HasColumnType("decimal(18,2)");

                            b1.HasKey("BeerId");

                            b1.ToTable("Beers");

                            b1.WithOwner()
                                .HasForeignKey("BeerId");
                        });

                    b.Navigation("CashBack")
                        .IsRequired();
                });

            modelBuilder.Entity("CashbackBeer.Domain.Entities.PartialSale", b =>
                {
                    b.HasOne("CashBackBeer.Domain.Entities.Beer", "Beer")
                        .WithMany()
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CashbackBeer.Domain.Entities.FinalSale", null)
                        .WithMany("PartialSales")
                        .HasForeignKey("FinalSaleId");

                    b.Navigation("Beer");
                });

            modelBuilder.Entity("CashbackBeer.Domain.Entities.FinalSale", b =>
                {
                    b.Navigation("PartialSales");
                });
#pragma warning restore 612, 618
        }
    }
}
