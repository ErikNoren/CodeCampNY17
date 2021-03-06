﻿// <auto-generated />
using EFCoreJson.Angular.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace EFCoreJson.Angular.Migrations
{
    [DbContext(typeof(CodeCampSampleContext))]
    [Migration("20171014074540_CreateDatabase")]
    partial class CreateDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EFCoreJson.Angular.Models.JsonComplexEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime?>("LastModifiedOn");

                    b.Property<decimal>("SchemaVersion")
                        .HasColumnType("decimal(9,5)");

                    b.Property<string>("SerializedData")
                        .IsRequired();

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("JsonComplexEntity");
                });

            modelBuilder.Entity("EFCoreJson.Angular.Models.JsonMixedTypesEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<decimal>("SchemaVersion")
                        .HasColumnType("decimal(9,5)");

                    b.Property<string>("SerializedAddresses");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("JsonMixedTypesEntity");
                });

            modelBuilder.Entity("EFCoreJson.Angular.Models.JsonPassThroughEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("SchemaVersion")
                        .HasColumnType("decimal(9,5)");

                    b.Property<string>("SerializedData")
                        .IsRequired();

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.ToTable("JsonPassThroughEntity");
                });
#pragma warning restore 612, 618
        }
    }
}
