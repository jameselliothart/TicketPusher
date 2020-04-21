﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TicketPusher.API.Data;

namespace TicketPusher.API.Migrations
{
    [DbContext(typeof(TicketPusherContext))]
    [Migration("20200421224757_AddProjects")]
    partial class AddProjects
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("TicketPusher.Domain.Projects.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("project_id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("projects");
                });

            modelBuilder.Entity("TicketPusher.Domain.Tickets.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ticket_id")
                        .HasColumnType("uuid");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasColumnName("owner")
                        .HasColumnType("text");

                    b.Property<Guid?>("ProjectId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("tickets");
                });

            modelBuilder.Entity("TicketPusher.Domain.Tickets.Ticket", b =>
                {
                    b.HasOne("TicketPusher.Domain.Projects.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.OwnsOne("TicketPusher.Domain.Common.TicketDetails", "TicketDetails", b1 =>
                        {
                            b1.Property<Guid>("TicketId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasColumnName("description")
                                .HasColumnType("text");

                            b1.Property<DateTime>("DueDate")
                                .HasColumnName("due_date")
                                .HasColumnType("timestamp without time zone");

                            b1.Property<DateTime>("SubmitDate")
                                .HasColumnName("submit_date")
                                .HasColumnType("timestamp without time zone");

                            b1.HasKey("TicketId");

                            b1.ToTable("tickets");

                            b1.WithOwner()
                                .HasForeignKey("TicketId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
