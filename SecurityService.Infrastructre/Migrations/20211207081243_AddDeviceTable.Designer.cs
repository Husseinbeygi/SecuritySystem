﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SecuritySystem.Infrastructre;

#nullable disable

namespace SecuritySystem.Infrastructre.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20211207081243_AddDeviceTable")]
    partial class AddDeviceTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("SecuritySystem.Domain.Device.Device", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceId")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Devices", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}