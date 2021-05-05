﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rule.WebAPI.Context;

namespace Rule.WebAPI.Migrations
{
    [DbContext(typeof(RuleDbContext))]
    [Migration("20210427121037_Initial_Create")]
    partial class Initial_Create
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Rule.WebAPI.Model.DTO.NRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("NRules");
                });

            modelBuilder.Entity("Rule.WebAPI.Model.Operation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Operations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "EqualTo"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Contains"
                        },
                        new
                        {
                            Id = 3,
                            Name = "StartsWith"
                        },
                        new
                        {
                            Id = 4,
                            Name = "EndsWith"
                        },
                        new
                        {
                            Id = 5,
                            Name = "NotEqualTo"
                        },
                        new
                        {
                            Id = 6,
                            Name = "GreaterThan"
                        },
                        new
                        {
                            Id = 7,
                            Name = "GreaterThanOrEqualTo"
                        },
                        new
                        {
                            Id = 8,
                            Name = "LessThan"
                        },
                        new
                        {
                            Id = 9,
                            Name = "LessThanOrEqualTo"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Between"
                        },
                        new
                        {
                            Id = 11,
                            Name = "IsNull"
                        },
                        new
                        {
                            Id = 12,
                            Name = "IsEmpty"
                        },
                        new
                        {
                            Id = 13,
                            Name = "IsNullOrWhiteSpace"
                        },
                        new
                        {
                            Id = 14,
                            Name = "IsNotNull"
                        },
                        new
                        {
                            Id = 15,
                            Name = "IsNotEmpty"
                        },
                        new
                        {
                            Id = 16,
                            Name = "IsNotNullNorWhiteSpace"
                        },
                        new
                        {
                            Id = 17,
                            Name = "In"
                        });
                });

            modelBuilder.Entity("Rule.WebAPI.Model.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsMale")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Rule.WebAPI.Model.RuleEngine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConnectorId")
                        .HasColumnType("int");

                    b.Property<int?>("NRuleId")
                        .HasColumnType("int");

                    b.Property<int>("OperationId")
                        .HasColumnType("int");

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ConnectorId");

                    b.HasIndex("NRuleId");

                    b.HasIndex("OperationId");

                    b.ToTable("RuleEngines");
                });

            modelBuilder.Entity("Rule.WebAPI.Model.StatementConnector", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StatementConnectors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "And"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Or"
                        });
                });

            modelBuilder.Entity("Rule.WebAPI.Model.RuleEngine", b =>
                {
                    b.HasOne("Rule.WebAPI.Model.StatementConnector", "Connector")
                        .WithMany()
                        .HasForeignKey("ConnectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rule.WebAPI.Model.DTO.NRule", "NRule")
                        .WithMany()
                        .HasForeignKey("NRuleId");

                    b.HasOne("Rule.WebAPI.Model.Operation", "Operation")
                        .WithMany()
                        .HasForeignKey("OperationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
