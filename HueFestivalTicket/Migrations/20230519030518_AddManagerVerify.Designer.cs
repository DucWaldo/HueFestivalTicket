﻿// <auto-generated />
using System;
using HueFestivalTicket.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HueFestivalTicket.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230519030518_AddManagerVerify")]
    partial class AddManagerVerify
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
                    b.Property<Guid>("IdAccount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdRole")
                        .HasColumnType("uniqueidentifier");

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

                    b.HasIndex("IdRole");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.Checkin", b =>
                {
                    b.Property<Guid>("IdCheckin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdAccount")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("QRCodeContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime>("TimeCheckin")
                        .HasColumnType("datetime2");

                    b.HasKey("IdCheckin");

                    b.HasIndex("IdAccount");

                    b.ToTable("Checkin");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.Customer", b =>
                {
                    b.Property<Guid>("IdCustomer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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
                    b.Property<Guid>("IdEvent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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
                    b.Property<Guid>("IdEventLocation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IdEvent")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdLocation")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("IdEventLocation");

                    b.HasIndex("IdEvent");

                    b.HasIndex("IdLocation");

                    b.ToTable("EventLocation");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.ImageEvent", b =>
                {
                    b.Property<Guid>("IdImageEvent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdEvent")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdImageEvent");

                    b.HasIndex("IdEvent");

                    b.ToTable("ImageEvent");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.Invoice", b =>
                {
                    b.Property<Guid>("IdInvoice")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdCustomer")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TimeCreate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdInvoice");

                    b.HasIndex("IdCustomer");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.Location", b =>
                {
                    b.Property<Guid>("IdLocation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Decription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdTypeLocation")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdLocation");

                    b.HasIndex("IdTypeLocation");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.ManagerToken", b =>
                {
                    b.Property<Guid>("IdRefreshToken")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExpiredAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IdAccount")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("IssuedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("JwtId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdRefreshToken");

                    b.HasIndex("IdAccount");

                    b.ToTable("ManagerToken");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.ManagerVerify", b =>
                {
                    b.Property<Guid>("IdVerifyCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime>("TimeCreate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeUpdate")
                        .HasColumnType("datetime2");

                    b.HasKey("IdVerifyCode");

                    b.ToTable("ManagerVerify");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.News", b =>
                {
                    b.Property<Guid>("IdNews")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdAccount")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeCreate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("IdNews");

                    b.HasIndex("IdAccount");

                    b.ToTable("News");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.PriceTicket", b =>
                {
                    b.Property<Guid>("IdPriceTicket")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdEventLocation")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdTypeTicket")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NumberSlot")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdPriceTicket");

                    b.HasIndex("IdEventLocation");

                    b.HasIndex("IdTypeTicket");

                    b.ToTable("PriceTicket");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.Role", b =>
                {
                    b.Property<Guid>("IdRole")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdRole");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.Support", b =>
                {
                    b.Property<Guid>("IdSuport")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdAccount")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("IdSuport");

                    b.HasIndex("IdAccount");

                    b.ToTable("Support");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.Ticket", b =>
                {
                    b.Property<Guid>("IdTicket")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdEventLocation")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdInvoice")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdTypeTicket")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("QRCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TicketNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeCreate")
                        .HasColumnType("datetime2");

                    b.HasKey("IdTicket");

                    b.HasIndex("IdEventLocation");

                    b.HasIndex("IdInvoice");

                    b.HasIndex("IdTypeTicket");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.TypeLocation", b =>
                {
                    b.Property<Guid>("IdTypeLocation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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
                    b.Property<Guid>("IdTypeTicket")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdTypeTicket");

                    b.ToTable("TypeTicket");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.User", b =>
                {
                    b.Property<Guid>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("IdAccount")
                        .HasColumnType("uniqueidentifier");

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

                    b.HasIndex("IdAccount");

                    b.ToTable("User");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.Account", b =>
                {
                    b.HasOne("HueFestivalTicket.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("IdRole")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.Checkin", b =>
                {
                    b.HasOne("HueFestivalTicket.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("IdAccount")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.EventLocation", b =>
                {
                    b.HasOne("HueFestivalTicket.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("IdEvent")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HueFestivalTicket.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("IdLocation")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.ImageEvent", b =>
                {
                    b.HasOne("HueFestivalTicket.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("IdEvent")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.Invoice", b =>
                {
                    b.HasOne("HueFestivalTicket.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("IdCustomer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.Location", b =>
                {
                    b.HasOne("HueFestivalTicket.Models.TypeLocation", "TypeLocation")
                        .WithMany()
                        .HasForeignKey("IdTypeLocation")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TypeLocation");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.ManagerToken", b =>
                {
                    b.HasOne("HueFestivalTicket.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("IdAccount")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.News", b =>
                {
                    b.HasOne("HueFestivalTicket.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("IdAccount")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.PriceTicket", b =>
                {
                    b.HasOne("HueFestivalTicket.Models.EventLocation", "EventLocation")
                        .WithMany()
                        .HasForeignKey("IdEventLocation")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HueFestivalTicket.Models.TypeTicket", "TypeTicket")
                        .WithMany()
                        .HasForeignKey("IdTypeTicket")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventLocation");

                    b.Navigation("TypeTicket");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.Support", b =>
                {
                    b.HasOne("HueFestivalTicket.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("IdAccount")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.Ticket", b =>
                {
                    b.HasOne("HueFestivalTicket.Models.EventLocation", "EventLocation")
                        .WithMany()
                        .HasForeignKey("IdEventLocation")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HueFestivalTicket.Models.Invoice", "Invoice")
                        .WithMany()
                        .HasForeignKey("IdInvoice")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HueFestivalTicket.Models.TypeTicket", "TypeTicket")
                        .WithMany()
                        .HasForeignKey("IdTypeTicket")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventLocation");

                    b.Navigation("Invoice");

                    b.Navigation("TypeTicket");
                });

            modelBuilder.Entity("HueFestivalTicket.Models.User", b =>
                {
                    b.HasOne("HueFestivalTicket.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("IdAccount")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });
#pragma warning restore 612, 618
        }
    }
}
