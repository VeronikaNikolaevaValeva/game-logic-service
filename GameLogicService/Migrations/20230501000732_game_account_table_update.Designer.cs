﻿// <auto-generated />
using System;
using GameLogicService.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GameLogicService.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230501000732_game_account_table_update")]
    partial class game_account_table_update
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GameLogicService.Models.Entity.GameAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("email_address");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "EmailAddress" }, "idx_account_emaild_address")
                        .IsUnique();

                    b.ToTable("game_account", (string)null);
                });

            modelBuilder.Entity("GameLogicService.Models.Entity.GameCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("category_name");

                    b.HasKey("Id");

                    b.ToTable("game_category", (string)null);
                });

            modelBuilder.Entity("GameLogicService.Models.Entity.GameCategoryGameAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GameAccountId")
                        .HasColumnType("int")
                        .HasColumnName("game_account_id");

                    b.Property<int>("GameCategoryId")
                        .HasColumnType("int")
                        .HasColumnName("game_category_id");

                    b.Property<int>("GameOptionsId")
                        .HasColumnType("int")
                        .HasColumnName("game_options_id");

                    b.HasKey("Id");

                    b.ToTable("game_category_game_account", (string)null);
                });

            modelBuilder.Entity("GameLogicService.Models.Entity.GameCategoryTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GameCategoryId")
                        .HasColumnType("int")
                        .HasColumnName("game_category_id");

                    b.Property<string>("GameCategoryTagName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("game_category_tag_name");

                    b.HasKey("Id");

                    b.HasIndex("GameCategoryId");

                    b.ToTable("game_category_tag", (string)null);
                });

            modelBuilder.Entity("GameLogicService.Models.Entity.GameOptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("QuestionAmount")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("question_amount");

                    b.Property<int?>("QuestionCategoryId")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("question_category_id");

                    b.Property<int>("QuestionDifficulty")
                        .HasColumnType("int")
                        .HasColumnName("question_difficulty");

                    b.Property<int?>("QuestionDifficultyId")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("question_difficulty_id");

                    b.HasKey("Id");

                    b.HasIndex("QuestionCategoryId");

                    b.ToTable("game_options", (string)null);
                });

            modelBuilder.Entity("GameLogicService.Models.Entity.PlayedQuiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("GameCategoryId")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("category_id");

                    b.Property<int?>("NumberOfCorrectAnswers")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("number_of_correct_answers");

                    b.Property<int?>("NumberOfIncorrectAnswers")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("number_of_incorrect_answers");

                    b.Property<int?>("NumberOfQuestions")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("number_of_questions");

                    b.HasKey("Id");

                    b.ToTable("played_quiz", (string)null);
                });

            modelBuilder.Entity("GameLogicService.Models.Entity.PlayedQuizGameAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GameAccountId")
                        .HasColumnType("int")
                        .HasColumnName("game_account_id");

                    b.Property<int>("PlayedQuizId")
                        .HasColumnType("int")
                        .HasColumnName("played_quiz_id");

                    b.HasKey("Id");

                    b.ToTable("played_quiz_game_account", (string)null);
                });

            modelBuilder.Entity("GameLogicService.Models.Entity.GameCategoryTag", b =>
                {
                    b.HasOne("GameLogicService.Models.Entity.GameCategory", null)
                        .WithMany("CategoryTags")
                        .HasForeignKey("GameCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameLogicService.Models.Entity.GameOptions", b =>
                {
                    b.HasOne("GameLogicService.Models.Entity.GameCategory", "QuestionCategory")
                        .WithMany()
                        .HasForeignKey("QuestionCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuestionCategory");
                });

            modelBuilder.Entity("GameLogicService.Models.Entity.GameCategory", b =>
                {
                    b.Navigation("CategoryTags");
                });
#pragma warning restore 612, 618
        }
    }
}
