using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QansDAL.Entities;

public partial class QansDbContext : DbContext
{
    public QansDbContext()
    {
    }

    public QansDbContext(DbContextOptions<QansDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Chapter> Chapters { get; set; }

    public virtual DbSet<Column> Columns { get; set; }

    public virtual DbSet<Option> Options { get; set; }

    public virtual DbSet<PasswordReset> PasswordResets { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Scenario> Scenarios { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=tcp:sqlservqansdev.database.windows.net,1433;Initial Catalog=sqldb-qans-dev-ind;Persist Security Info=False;User ID=qans-admin;Password=ansQuestion@4024;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Answers__D4825024386C4CC1");

            entity.Property(e => e.AnswerId)
                .ValueGeneratedNever()
                .HasColumnName("AnswerID");
            entity.Property(e => e.ColumnId).HasColumnName("ColumnID");
            entity.Property(e => e.MatchText)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.Column).WithMany(p => p.Answers)
                .HasForeignKey(d => d.ColumnId)
                .HasConstraintName("FK__Answers__ColumnI__04E4BC85");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__Answers__questio__03F0984C");
        });

        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.HasKey(e => e.ChapterId).HasName("PK__Chapters__745EFE87C7DEC71C");

            entity.Property(e => e.ChapterId).HasColumnName("chapter_id");
            entity.Property(e => e.ChapterName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("chapter_name");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.TopicId).HasColumnName("topic_id");

            entity.HasOne(d => d.Topic).WithMany(p => p.Chapters)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("FK__Chapters__topic___72C60C4A");
        });

        modelBuilder.Entity<Column>(entity =>
        {
            entity.HasKey(e => e.ColumnId).HasName("PK__Columns__1AA1422F1A4C47F2");

            entity.Property(e => e.ColumnId)
                .ValueGeneratedNever()
                .HasColumnName("ColumnID");
            entity.Property(e => e.ColumnText)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.Question).WithMany(p => p.Columns)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__Columns__questio__01142BA1");
        });

        modelBuilder.Entity<Option>(entity =>
        {
            entity.HasKey(e => e.OptionId).HasName("PK__Options__F4EACE1BCE9447EA");

            entity.Property(e => e.OptionId).HasColumnName("option_id");
            entity.Property(e => e.IsCorrect).HasColumnName("is_correct");
            entity.Property(e => e.OptionText)
                .HasColumnType("text")
                .HasColumnName("option_text");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.Question).WithMany(p => p.Options)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__Options__questio__7B5B524B");
        });

        modelBuilder.Entity<PasswordReset>(entity =>
        {
            entity.HasKey(e => e.ResetId).HasName("PK__Password__40FB0520A7A3A54B");

            entity.ToTable("Password_Resets");

            entity.HasIndex(e => e.ResetToken, "UQ__Password__25F405EB9B26F64C").IsUnique();

            entity.Property(e => e.ResetId).HasColumnName("reset_id");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("datetime")
                .HasColumnName("expires_at");
            entity.Property(e => e.ResetToken)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("reset_token");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.PasswordResets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Password___user___6E01572D");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__2EC215497E3CFEAA");

            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.ChapterId).HasColumnName("chapter_id");
            entity.Property(e => e.QuestionText)
                .HasColumnType("text")
                .HasColumnName("question_text");
            entity.Property(e => e.QuestionType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("question_type");
            entity.Property(e => e.ReferenceImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("reference_image_url");
            entity.Property(e => e.ReferenceLink)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("reference_link");
            entity.Property(e => e.ScenarioId).HasColumnName("scenario_id");

            entity.HasOne(d => d.Chapter).WithMany(p => p.Questions)
                .HasForeignKey(d => d.ChapterId)
                .HasConstraintName("FK__Questions__chapt__778AC167");

            entity.HasOne(d => d.Scenario).WithMany(p => p.Questions)
                .HasForeignKey(d => d.ScenarioId)
                .HasConstraintName("FK__Questions__scena__787EE5A0");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__760965CCB37787C3");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__783254B196C036DC").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Scenario>(entity =>
        {
            entity.HasKey(e => e.ScenarioId).HasName("PK__Scenario__D56D0C7D6657DD17");

            entity.Property(e => e.ScenarioId).HasColumnName("scenario_id");
            entity.Property(e => e.ReferenceImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("reference_image_url");
            entity.Property(e => e.ReferenceLink)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("reference_link");
            entity.Property(e => e.ScenarioText)
                .HasColumnType("text")
                .HasColumnName("scenario_text");
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.TopicId).HasName("PK__Topics__D5DAA3E9998B3BB4");

            entity.Property(e => e.TopicId).HasColumnName("topic_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.TopicName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("topic_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F76984E73");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E616430ADA0A1").IsUnique();

            entity.HasIndex(e => e.MobileNo, "UQ__Users__D7B19EFAFC3C963D").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC57214EB7669").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("mobile_no");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__User_Role__role___6A30C649"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__User_Role__user___693CA210"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK__User_Rol__6EDEA15313BDF757");
                        j.ToTable("User_Roles");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
