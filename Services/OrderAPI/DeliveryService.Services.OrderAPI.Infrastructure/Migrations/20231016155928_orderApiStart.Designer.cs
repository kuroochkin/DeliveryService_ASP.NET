﻿// <auto-generated />
using System;
using DeliveryService.Services.OrderAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DeliveryService.Services.OrderAPI.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231016155928_orderApiStart")]
    partial class orderApiStart
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DeliveryService.Services.OrderAPI.Domain.Order.OrderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ConfirmedCourier")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ConfirmedRestaurant")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("CourierId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("End")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EndRestaurant")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ManagerId")
                        .HasColumnType("uuid");

                    b.Property<bool>("PaymentStatus")
                        .HasColumnType("boolean");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DeliveryService.Services.OrderAPI.Domain.OrderItem.OrderItemEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<Guid?>("OrderEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<string>("Thumbnail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("OrderEntityId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("DeliveryService.Services.OrderAPI.Domain.OrderItem.OrderItemEntity", b =>
                {
                    b.HasOne("DeliveryService.Services.OrderAPI.Domain.Order.OrderEntity", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderEntityId");
                });

            modelBuilder.Entity("DeliveryService.Services.OrderAPI.Domain.Order.OrderEntity", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
