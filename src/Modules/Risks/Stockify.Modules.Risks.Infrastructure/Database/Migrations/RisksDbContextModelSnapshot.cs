﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Stockify.Modules.Risks.Infrastructure.Database;

#nullable disable

namespace Stockify.Modules.Risks.Infrastructure.Database.Migrations
{
    [DbContext(typeof(RisksDbContext))]
    partial class RisksDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("risks")
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("QuestionSession", b =>
                {
                    b.Property<Guid>("QuestionsId")
                        .HasColumnType("uuid")
                        .HasColumnName("questions_id");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("uuid")
                        .HasColumnName("session_id");

                    b.HasKey("QuestionsId", "SessionId")
                        .HasName("pk_session_questions");

                    b.HasIndex("SessionId")
                        .HasDatabaseName("ix_session_questions_session_id");

                    b.ToTable("session_questions", "risks");
                });

            modelBuilder.Entity("Stockify.Common.Infrastructure.Inbox.InboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("jsonb")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("text")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("occurred_on_utc");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("processed_on_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_inbox_messages");

                    b.ToTable("inbox_messages", "risks");
                });

            modelBuilder.Entity("Stockify.Common.Infrastructure.Inbox.InboxMessageConsumer", b =>
                {
                    b.Property<Guid>("InboxMessageId")
                        .HasColumnType("uuid")
                        .HasColumnName("inbox_message_id");

                    b.Property<string>("Name")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("name");

                    b.HasKey("InboxMessageId", "Name")
                        .HasName("pk_inbox_message_consumers");

                    b.ToTable("inbox_message_consumers", "risks");
                });

            modelBuilder.Entity("Stockify.Common.Infrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("jsonb")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("text")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("occurred_on_utc");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("processed_on_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_outbox_messages");

                    b.ToTable("outbox_messages", "risks");
                });

            modelBuilder.Entity("Stockify.Common.Infrastructure.Outbox.OutboxMessageConsumer", b =>
                {
                    b.Property<Guid>("OutboxMessageId")
                        .HasColumnType("uuid")
                        .HasColumnName("outbox_message_id");

                    b.Property<string>("Name")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("name");

                    b.HasKey("OutboxMessageId", "Name")
                        .HasName("pk_outbox_message_consumers");

                    b.ToTable("outbox_message_consumers", "risks");
                });

            modelBuilder.Entity("Stockify.Modules.Risks.Domain.Individuals.Individual", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("last_name");

                    b.HasKey("Id")
                        .HasName("pk_individuals");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_individuals_email");

                    b.ToTable("individuals", "risks");
                });

            modelBuilder.Entity("Stockify.Modules.Risks.Domain.Questions.Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("content");

                    b.Property<int>("Points")
                        .HasColumnType("integer")
                        .HasColumnName("points");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uuid")
                        .HasColumnName("question_id");

                    b.HasKey("Id")
                        .HasName("pk_answers");

                    b.HasIndex("QuestionId")
                        .HasDatabaseName("ix_answers_question_id");

                    b.ToTable("answers", "risks", t =>
                        {
                            t.HasCheckConstraint("CK_Points_NotNegative", "points >= 0");
                        });
                });

            modelBuilder.Entity("Stockify.Modules.Risks.Domain.Questions.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("content");

                    b.HasKey("Id")
                        .HasName("pk_questions");

                    b.ToTable("questions", "risks");
                });

            modelBuilder.Entity("Stockify.Modules.Risks.Domain.Sessions.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("CompletedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("completed_at_utc");

                    b.Property<Guid>("IndividualId")
                        .HasColumnType("uuid")
                        .HasColumnName("individual_id");

                    b.Property<DateTime?>("StartedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("started_at_utc");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_sessions");

                    b.HasIndex("IndividualId")
                        .HasDatabaseName("ix_sessions_individual_id");

                    b.ToTable("sessions", "risks");
                });

            modelBuilder.Entity("QuestionSession", b =>
                {
                    b.HasOne("Stockify.Modules.Risks.Domain.Questions.Question", null)
                        .WithMany()
                        .HasForeignKey("QuestionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_session_questions_questions_questions_id");

                    b.HasOne("Stockify.Modules.Risks.Domain.Sessions.Session", null)
                        .WithMany()
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_session_questions_sessions_session_id");
                });

            modelBuilder.Entity("Stockify.Modules.Risks.Domain.Questions.Answer", b =>
                {
                    b.HasOne("Stockify.Modules.Risks.Domain.Questions.Question", null)
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_answers_questions_question_id");
                });

            modelBuilder.Entity("Stockify.Modules.Risks.Domain.Sessions.Session", b =>
                {
                    b.HasOne("Stockify.Modules.Risks.Domain.Individuals.Individual", null)
                        .WithMany()
                        .HasForeignKey("IndividualId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_sessions_individuals_individual_id");
                });

            modelBuilder.Entity("Stockify.Modules.Risks.Domain.Questions.Question", b =>
                {
                    b.Navigation("Answers");
                });
#pragma warning restore 612, 618
        }
    }
}
