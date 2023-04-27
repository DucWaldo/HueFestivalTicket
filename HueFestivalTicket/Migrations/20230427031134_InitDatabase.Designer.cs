﻿// <auto-generated />
using System;
using HueFestivalTicket.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HueFestivalTicket.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230427031134_InitDatabase")]
    partial class InitDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HueFestivalTicket.Models.Account", b =>
                {
                    b.Property<int>("IdAccount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAccount"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeJoined")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("IdAccount");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.AccountRole", b =>
                {
                    b.Property<int>("IdAccountRole")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAccountRole"));

                    b.Property<int?>("AccountIdAccount")
                        .HasColumnType("int");

                    b.Property<int>("IdAccount")
                        .HasColumnType("int");

                    b.Property<int>("IdRole")
                        .HasColumnType("int");

                    b.Property<int?>("RoleIdRole")
                        .HasColumnType("int");

                    b.HasKey("IdAccountRole");

                    b.HasIndex("AccountIdAccount");

                    b.HasIndex("RoleIdRole");

                    b.ToTable("AccountRole");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.Checkin", b =>
                {
                    b.Property<int>("IdCheckin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCheckin"));

                    b.Property<int?>("AccountIdAccount")
                        .HasColumnType("int");

                    b.Property<int>("IdAccount")
                        .HasColumnType("int");

                    b.Property<int>("IdTicket")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int?>("TicketIdTicket")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeCheckin")
                        .HasColumnType("datetime2");

                    b.HasKey("IdCheckin");

                    b.HasIndex("AccountIdAccount");

                    b.HasIndex("TicketIdTicket");

                    b.ToTable("Checkin");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.Customer", b =>
                {
                    b.Property<int>("IdCustomer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCustomer"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("IdCard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCustomer");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.Event", b =>
                {
                    b.Property<int>("IdEvent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEvent"));

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("StatusTicket")
                        .HasColumnType("bit");

                    b.Property<int>("TypeEvent")
                        .HasColumnType("int");

                    b.HasKey("IdEvent");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.EventLocation", b =>
                {
                    b.Property<int>("IdEventLocation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEventLocation"));

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EventIdEvent")
                        .HasColumnType("int");

                    b.Property<int>("IdEvent")
                        .HasColumnType("int");

                    b.Property<int>("IdLocation")
                        .HasColumnType("int");

                    b.Property<int?>("LocationIdLocation")
                        .HasColumnType("int");

                    b.Property<int>("NumberSlot")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("IdEventLocation");

                    b.HasIndex("EventIdEvent");

                    b.HasIndex("LocationIdLocation");

                    b.ToTable("EventLocation");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.ImageEvent", b =>
                {
                    b.Property<int>("IdImageEvent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdImageEvent"));

                    b.Property<int?>("EventIdEvent")
                        .HasColumnType("int");

                    b.Property<int>("IdEvent")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdImageEvent");

                    b.HasIndex("EventIdEvent");

                    b.ToTable("ImageEvent");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.Location", b =>
                {
                    b.Property<int>("IdLocation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdLocation"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Decription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdTypeLocation")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TypeLocationIdTypeLocation")
                        .HasColumnType("int");

                    b.HasKey("IdLocation");

                    b.HasIndex("TypeLocationIdTypeLocation");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.News", b =>
                {
                    b.Property<int>("IdNews")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdNews"));

                    b.Property<int?>("AccountIdAccount")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdAccount")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeCreate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("IdNews");

                    b.HasIndex("AccountIdAccount");

                    b.ToTable("News");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.PriceTicket", b =>
                {
                    b.Property<int>("IdPriceTicket")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPriceTicket"));

                    b.Property<int?>("EventLocationIdEventLocation")
                        .HasColumnType("int");

                    b.Property<int>("IdEventLocation")
                        .HasColumnType("int");

                    b.Property<int>("IdTypeTicket")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("TypeTicketIdTypeTicket")
                        .HasColumnType("int");

                    b.HasKey("IdPriceTicket");

                    b.HasIndex("EventLocationIdEventLocation");

                    b.HasIndex("TypeTicketIdTypeTicket");

                    b.ToTable("PriceTicket");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.Role", b =>
                {
                    b.Property<int>("IdRole")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRole"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdRole");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.Support", b =>
                {
                    b.Property<int>("IdSuport")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSuport"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("IdSuport");

                    b.ToTable("Support");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.Ticket", b =>
                {
                    b.Property<int>("IdTicket")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTicket"));

                    b.Property<int?>("CustomerIdCustomer")
                        .HasColumnType("int");

                    b.Property<int?>("EventLocationIdEventLocation")
                        .HasColumnType("int");

                    b.Property<int>("IdCustomer")
                        .HasColumnType("int");

                    b.Property<int>("IdEventLocation")
                        .HasColumnType("int");

                    b.Property<int>("IdTypeTicket")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("QRCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TicketNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TypeTicketIdTypeTicket")
                        .HasColumnType("int");

                    b.HasKey("IdTicket");

                    b.HasIndex("CustomerIdCustomer");

                    b.HasIndex("EventLocationIdEventLocation");

                    b.HasIndex("TypeTicketIdTypeTicket");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.TypeLocation", b =>
                {
                    b.Property<int>("IdTypeLocation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTypeLocation"));

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTypeLocation");

                    b.ToTable("TypeLocation");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.TypeTicket", b =>
                {
                    b.Property<int>("IdTypeTicket")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTypeTicket"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdTypeTicket");

                    b.ToTable("TypeTicket");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUser"));

                    b.Property<int?>("AccountIdAccount")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("IdAccount")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Organization")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUser");

                    b.HasIndex("AccountIdAccount");

                    b.ToTable("User");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.AccountRole", b =>
                {
                    b.HasOne("HueFestivalTicket.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountIdAccount");

                    b.HasOne("HueFestivalTicket.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleIdRole");

                    b.Navigation("Account");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.Checkin", b =>
                {
                    b.HasOne("HueFestivalTicket.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountIdAccount");

                    b.HasOne("HueFestivalTicket.Models.Ticket", "Ticket")
                        .WithMany()
                        .HasForeignKey("TicketIdTicket");

                    b.Navigation("Account");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.EventLocation", b =>
                {
                    b.HasOne("HueFestivalTicket.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventIdEvent");

                    b.HasOne("HueFestivalTicket.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationIdLocation");

                    b.Navigation("Event");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.ImageEvent", b =>
                {
                    b.HasOne("HueFestivalTicket.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventIdEvent");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.Location", b =>
                {
                    b.HasOne("HueFestivalTicket.Models.TypeLocation", "TypeLocation")
                        .WithMany()
                        .HasForeignKey("TypeLocationIdTypeLocation");

                    b.Navigation("TypeLocation");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.News", b =>
                {
                    b.HasOne("HueFestivalTicket.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountIdAccount");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.PriceTicket", b =>
                {
                    b.HasOne("HueFestivalTicket.Models.EventLocation", "EventLocation")
                        .WithMany()
                        .HasForeignKey("EventLocationIdEventLocation");

                    b.HasOne("HueFestivalTicket.Models.TypeTicket", "TypeTicket")
                        .WithMany()
                        .HasForeignKey("TypeTicketIdTypeTicket");

                    b.Navigation("EventLocation");

                    b.Navigation("TypeTicket");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.Ticket", b =>
                {
                    b.HasOne("HueFestivalTicket.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerIdCustomer");

                    b.HasOne("HueFestivalTicket.Models.EventLocation", "EventLocation")
                        .WithMany()
                        .HasForeignKey("EventLocationIdEventLocation");

                    b.HasOne("HueFestivalTicket.Models.TypeTicket", "TypeTicket")
                        .WithMany()
                        .HasForeignKey("TypeTicketIdTypeTicket");

                    b.Navigation("Customer");

                    b.Navigation("EventLocation");

                    b.Navigation("TypeTicket");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.User", b =>
                {
                    b.HasOne("HueFestivalTicket.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountIdAccount");

                    b.Navigation("Account");
                });
#pragma warning restore 612, 618
        }
    }
}
