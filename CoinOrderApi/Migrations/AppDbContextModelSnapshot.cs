﻿// <auto-generated />
using System;
using CoinOrderApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoinOrderApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CoinOrderApi.Data.Models.CoinOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedDate");

                    b.HasIndex("Id");

                    b.HasIndex("UserId", "DeletedDate");

                    b.ToTable("CoinOrder");
                });

            modelBuilder.Entity("CoinOrderApi.Data.Models.CommunicationPermission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CoinOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Email")
                        .HasColumnType("bit");

                    b.Property<bool>("PushNotification")
                        .HasColumnType("bit");

                    b.Property<bool>("Sms")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CoinOrderId")
                        .IsUnique();

                    b.HasIndex("Id");

                    b.ToTable("CommunicationPermission");
                });

            modelBuilder.Entity("CoinOrderApi.Data.Models.EmailMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CoinOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EnqueuedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CoinOrderId");

                    b.HasIndex("Id");

                    b.ToTable("EmailMessage");
                });

            modelBuilder.Entity("CoinOrderApi.Data.Models.MessageTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProcessType")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("MessageTemplate");
                });

            modelBuilder.Entity("CoinOrderApi.Data.Models.PushNotificationMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CoinOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EnqueuedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReceiverId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CoinOrderId");

                    b.HasIndex("Id");

                    b.ToTable("PushNotificationMessages");
                });

            modelBuilder.Entity("CoinOrderApi.Data.Models.SmsMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CoinOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EnqueuedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CoinOrderId");

                    b.HasIndex("Id");

                    b.ToTable("SmsMessage");
                });

            modelBuilder.Entity("CoinOrderApi.Data.Models.CommunicationPermission", b =>
                {
                    b.HasOne("CoinOrderApi.Data.Models.CoinOrder", "CoinOrder")
                        .WithOne("CommunicationPermission")
                        .HasForeignKey("CoinOrderApi.Data.Models.CommunicationPermission", "CoinOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CoinOrder");
                });

            modelBuilder.Entity("CoinOrderApi.Data.Models.EmailMessage", b =>
                {
                    b.HasOne("CoinOrderApi.Data.Models.CoinOrder", "CoinOrder")
                        .WithMany("EmailMessages")
                        .HasForeignKey("CoinOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CoinOrder");
                });

            modelBuilder.Entity("CoinOrderApi.Data.Models.PushNotificationMessage", b =>
                {
                    b.HasOne("CoinOrderApi.Data.Models.CoinOrder", "CoinOrder")
                        .WithMany("PushNotificationMessages")
                        .HasForeignKey("CoinOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CoinOrder");
                });

            modelBuilder.Entity("CoinOrderApi.Data.Models.SmsMessage", b =>
                {
                    b.HasOne("CoinOrderApi.Data.Models.CoinOrder", "CoinOrder")
                        .WithMany("SmsMessages")
                        .HasForeignKey("CoinOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CoinOrder");
                });

            modelBuilder.Entity("CoinOrderApi.Data.Models.CoinOrder", b =>
                {
                    b.Navigation("CommunicationPermission")
                        .IsRequired();

                    b.Navigation("EmailMessages");

                    b.Navigation("PushNotificationMessages");

                    b.Navigation("SmsMessages");
                });
#pragma warning restore 612, 618
        }
    }
}
