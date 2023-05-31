using GameLogicService.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace GameLogicService.DataContext
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GameAccount> GameAccounts { get; set; } = null!;
        public virtual DbSet<GameOptions> GameOptions { get; set; } = null!;
        public virtual DbSet<GameCategory> GameCategory { get; set; } = null!;
        public virtual DbSet<GameCategoryTag> GameCategoryTag { get; set; } = null!;
        public virtual DbSet<GameCategoryGameAccount> GameCategoryGameAccount { get; set; } = null!;
        public virtual DbSet<PlayedQuiz> PlayedQuiz { get; set; } = null!;
        public virtual DbSet<PlayedQuizGameAccount> PlayedQuizGameAccount { get; set; } = null!;
        public virtual DbSet<GameAccountAuth> GameAccountAuth { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            string IdColumnType = "int";

            //Table Game Account
            modelBuilder.Entity<GameAccount>(entity =>
            {
                entity.ToTable("game_account");

                entity.HasIndex(e => e.EmailAddress, "idx_account_emaild_address")
                    .IsUnique();

                entity.Property(e => e.Id)
                   .IsRequired()
                   .HasColumnType(IdColumnType)
                   .HasColumnName("id");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasColumnName("email_address");

            });

            //Table Game Options
            modelBuilder.Entity<GameOptions>(entity =>
            {
                entity.ToTable("game_options");

                entity.Property(e => e.Id)
                   .IsRequired()
                   .HasColumnType(IdColumnType)
                   .HasColumnName("id");

                entity.Property(e => e.QuestionDifficultyId)
                    .IsRequired()
                    .HasColumnName("question_difficulty_id");

                entity.Property(e => e.QuestionCategoryId)
                    .IsRequired()
                    .HasColumnName("question_category_id");

                entity.Property(e => e.QuestionAmount)
                .IsRequired()
                .HasColumnName("question_amount");

                entity.Property(e => e.QuestionDifficulty)
                .IsRequired()
                .HasColumnName("question_difficulty");
            });

            //Table Game Category
            modelBuilder.Entity<GameCategory>(entity =>
            {
                entity.ToTable("game_category");

                entity.Property(e => e.Id)
                   .IsRequired()
                   .HasColumnType(IdColumnType)
                   .HasColumnName("id");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnName("category_name");
            });

            //Table Game Category Tag
            modelBuilder.Entity<GameCategoryTag>(entity =>
            {
                entity.ToTable("game_category_tag");

                entity.Property(e => e.Id)
                   .IsRequired()
                   .HasColumnType(IdColumnType)
                   .HasColumnName("id");

                entity.Property(e => e.GameCategoryId)
                    .IsRequired()
                    .HasColumnName("game_category_id");

                entity.Property(e => e.GameCategoryTagName)
                    .IsRequired()
                    .HasColumnName("game_category_tag_name");
            });

            //Game Category Game Account
            modelBuilder.Entity<GameCategoryGameAccount>(entity =>
            {
                entity.ToTable("game_category_game_account");

                entity.Property(e => e.Id)
                   .IsRequired()
                   .HasColumnType(IdColumnType)
                   .HasColumnName("id");

                entity.Property(e => e.GameAccountId)
                    .IsRequired()
                    .HasColumnName("game_account_id");

                entity.Property(e => e.GameCategoryId)
                    .IsRequired()
                    .HasColumnName("game_category_id");
                
                entity.Property(e => e.GameOptionsId)
                    .IsRequired()
                    .HasColumnName("game_options_id");
            });
            
            //Played Quiz
            modelBuilder.Entity<PlayedQuiz>(entity =>
            {
                entity.ToTable("played_quiz");

                entity.Property(e => e.Id)
                   .IsRequired()
                   .HasColumnType(IdColumnType)
                   .HasColumnName("id");

                entity.Property(e => e.NumberOfQuestions)
                    .IsRequired()
                    .HasColumnName("number_of_questions");
                entity.Property(e => e.NumberOfCorrectAnswers)
                    .IsRequired()
                    .HasColumnName("number_of_correct_answers");
                entity.Property(e => e.NumberOfIncorrectAnswers)
                    .IsRequired()
                    .HasColumnName("number_of_incorrect_answers");
                entity.Property(e => e.GameCategoryId)
                    .IsRequired()
                    .HasColumnName("category_id");
            });

            //Played Quiz Game Account
            modelBuilder.Entity<PlayedQuizGameAccount>(entity =>
            {
                entity.ToTable("played_quiz_game_account");

                entity.Property(e => e.Id)
                   .IsRequired()
                   .HasColumnType(IdColumnType)
                   .HasColumnName("id");

                entity.Property(e => e.GameAccountId)
                    .IsRequired()
                    .HasColumnName("game_account_id");

                entity.Property(e => e.PlayedQuizId)
                    .IsRequired()
                    .HasColumnName("played_quiz_id");
            });
            
            //Played Quiz Game Account Auth ID
            modelBuilder.Entity<GameAccountAuth>(entity =>
            {
                entity.ToTable("game_account_auth_id");

                entity.Property(e => e.Id)
                   .IsRequired()
                   .HasColumnType(IdColumnType)
                   .HasColumnName("id");

                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasColumnName("account_id");

                entity.Property(e => e.AuthId)
                    .IsRequired()
                    .HasColumnName("auth_id");
            });
        }
    }
}
