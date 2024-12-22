using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Larionov_vi.Models
{
    public partial class VicourselaContext : DbContext
    {
        public VicourselaContext()
        {
        }

        public VicourselaContext(DbContextOptions<VicourselaContext> options)
            : base(options)
        {
        }

        
        public virtual DbSet<Группы> Группыs { get; set; } = null!; 
        public virtual DbSet<Задания> Заданияs { get; set; } = null!;
        public virtual DbSet<Занятия> Занятияs { get; set; } = null!;
        public virtual DbSet<Зачисления> Зачисленияs { get; set; } = null!;
        public virtual DbSet<Курсы> Курсыs { get; set; } = null!;
        public virtual DbSet<Пользователи> Пользователиs { get; set; } = null!;
        public virtual DbSet<Посещаемость> Посещаемостьs { get; set; } = null!;
        public virtual DbSet<Программы> Программыs { get; set; } = null!;
        public virtual DbSet<ПрограммыКурсов> ПрограммыКурсовs { get; set; } = null!;
        public virtual DbSet<Работы> Работыs { get; set; } = null!;
        public virtual DbSet<Роли> Ролиs { get; set; } = null!;
        public virtual DbSet<События> Событияs { get; set; } = null!;
        public virtual DbSet<Студенты> Студентыs { get; set; } = null!;
        public virtual DbSet<УчастникиСобытий> УчастникиСобытийs { get; set; } = null!;
        public virtual DbSet<Факультеты> Факультетыs { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Группы>(entity =>
            {
                entity.HasKey(e => e.КодГруппы)
                    .HasName("PK__Группы__C53084C65915E8B2");

                entity.ToTable("Группы");

                entity.HasIndex(e => e.НазваниеГруппы, "UQ__Группы__AC501ED3E29F7299")
                    .IsUnique();

                entity.Property(e => e.НазваниеГруппы).HasMaxLength(50);

                entity.Property(e => e.Специализация).HasMaxLength(100);
            });

            modelBuilder.Entity<Задания>(entity =>
            {
                entity.HasKey(e => e.КодЗадания)
                    .HasName("PK__Задания__1AB5BEBC25EA9CA3");

                entity.ToTable("Задания");

                entity.Property(e => e.ДатаСдачи).HasColumnType("date");

                entity.Property(e => e.Название).HasMaxLength(100);

                entity.HasOne(d => d.КодКурсаNavigation)
                    .WithMany(p => p.Заданияs)
                    .HasForeignKey(d => d.КодКурса)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Задания__КодКурс__03F0984C");
            });

            modelBuilder.Entity<Занятия>(entity =>
            {
                entity.HasKey(e => e.КодЗанятия)
                    .HasName("PK__Занятия__8CA853B4DDE2107E");

                entity.ToTable("Занятия");

                entity.Property(e => e.Аудитория).HasMaxLength(20);

                entity.Property(e => e.ДатаЗанятия).HasColumnType("date");

                entity.HasOne(d => d.КодКурсаNavigation)
                    .WithMany(p => p.Занятияs)
                    .HasForeignKey(d => d.КодКурса)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Занятия__КодКурс__7D439ABD");
            });

            modelBuilder.Entity<Зачисления>(entity =>
            {
                entity.HasKey(e => e.КодЗачисления)
                    .HasName("PK__Зачислен__2D95BA62E9DF52BF");

                entity.ToTable("Зачисления");

                entity.Property(e => e.ДатаЗачисления).HasColumnType("date");

                entity.HasOne(d => d.КодКурсаNavigation)
                    .WithMany(p => p.Зачисленияs)
                    .HasForeignKey(d => d.КодКурса)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Зачислени__КодКу__7A672E12");

                entity.HasOne(d => d.КодСтудентаNavigation)
                    .WithMany(p => p.Зачисленияs)
                    .HasForeignKey(d => d.КодСтудента)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Зачислени__КодСт__797309D9");
            });

            modelBuilder.Entity<Курсы>(entity =>
            {
                entity.HasKey(e => e.КодКурса)
                    .HasName("PK__Курсы__5E122F8D19B1343B");

                entity.ToTable("Курсы");

                entity.Property(e => e.НазваниеКурса).HasMaxLength(100);

                entity.HasOne(d => d.КодПреподавателяNavigation)
                    .WithMany(p => p.Курсыs)
                    .HasForeignKey(d => d.КодПреподавателя)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Курсы__КодПрепод__72C60C4A");
            });

            modelBuilder.Entity<Пользователи>(entity =>
            {
                entity.HasKey(e => e.КодПользователя)
                    .HasName("PK__Пользова__200434A2F1FCAB7E");

                entity.ToTable("Пользователи");

                entity.HasIndex(e => e.ЭлектроннаяПочта, "UQ__Пользова__372226199806A638")
                    .IsUnique();

                entity.Property(e => e.Имя).HasMaxLength(50);

                entity.Property(e => e.Фамилия).HasMaxLength(50);

                entity.Property(e => e.ХэшПароля).HasMaxLength(256);

                entity.Property(e => e.ЭлектроннаяПочта).HasMaxLength(100);

                entity.HasOne(d => d.КодРолиNavigation)
                    .WithMany(p => p.Пользователиs)
                    .HasForeignKey(d => d.КодРоли)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Пользоват__КодРо__6383C8BA");
            });

            modelBuilder.Entity<Посещаемость>(entity =>
            {
                entity.HasKey(e => e.КодПосещаемости)
                    .HasName("PK__Посещаем__40245443CD5E7E20");

                entity.ToTable("Посещаемость");

                entity.Property(e => e.СтатусПосещаемости).HasMaxLength(20);

                entity.HasOne(d => d.КодЗанятияNavigation)
                    .WithMany(p => p.Посещаемостьs)
                    .HasForeignKey(d => d.КодЗанятия)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Посещаемо__КодЗа__00200768");

                entity.HasOne(d => d.КодСтудентаNavigation)
                    .WithMany(p => p.Посещаемостьs)
                    .HasForeignKey(d => d.КодСтудента)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Посещаемо__КодСт__01142BA1");
            });

            modelBuilder.Entity<Программы>(entity =>
            {
                entity.HasKey(e => e.КодПрограммы)
                    .HasName("PK__Программ__8D5DBFABF25D6D56");

                entity.ToTable("Программы");

                entity.Property(e => e.НазваниеПрограммы).HasMaxLength(100);

                entity.HasOne(d => d.КодФакультетаNavigation)
                    .WithMany(p => p.Программыs)
                    .HasForeignKey(d => d.КодФакультета)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Программы__КодФа__693CA210");
            });

            modelBuilder.Entity<ПрограммыКурсов>(entity =>
            {
                entity.HasKey(e => e.КодПрограммыКурса)
                    .HasName("PK__Программ__4FC961AA7A903F7C");

                entity.ToTable("ПрограммыКурсов");

                entity.HasOne(d => d.КодКурсаNavigation)
                    .WithMany(p => p.ПрограммыКурсовs)
                    .HasForeignKey(d => d.КодКурса)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Программы__КодКу__75A278F5");

                entity.HasOne(d => d.КодПрограммыNavigation)
                    .WithMany(p => p.ПрограммыКурсовs)
                    .HasForeignKey(d => d.КодПрограммы)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Программы__КодПр__76969D2E");
            });

            modelBuilder.Entity<Работы>(entity =>
            {
                entity.HasKey(e => e.КодРаботы)
                    .HasName("PK__Работы__76A3F6880D1CB017");

                entity.ToTable("Работы");

                entity.Property(e => e.ДатаСдачи).HasColumnType("date");

                entity.Property(e => e.Оценка).HasMaxLength(10);

                entity.HasOne(d => d.КодЗаданияNavigation)
                    .WithMany(p => p.Работыs)
                    .HasForeignKey(d => d.КодЗадания)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Работы__КодЗадан__06CD04F7");

                entity.HasOne(d => d.КодСтудентаNavigation)
                    .WithMany(p => p.Работыs)
                    .HasForeignKey(d => d.КодСтудента)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Работы__КодСтуде__07C12930");
            });

            modelBuilder.Entity<Роли>(entity =>
            {
                entity.HasKey(e => e.КодРоли)
                    .HasName("PK__Роли__A2517212851D6471");

                entity.ToTable("Роли");

                entity.HasIndex(e => e.НазваниеРоли, "UQ__Роли__B867938ED5819691")
                    .IsUnique();

                entity.Property(e => e.НазваниеРоли).HasMaxLength(50);
            });

            modelBuilder.Entity<События>(entity =>
            {
                entity.HasKey(e => e.КодСобытия)
                    .HasName("PK__События__D78AD13A7A39964B");

                entity.ToTable("События");

                entity.Property(e => e.ДатаСобытия).HasColumnType("date");

                entity.Property(e => e.МестоПроведения).HasMaxLength(100);

                entity.Property(e => e.НазваниеСобытия).HasMaxLength(100);
            });

            modelBuilder.Entity<Студенты>(entity =>
            {
                entity.HasKey(e => e.КодСтудента)
                    .HasName("PK__Студенты__4036A07509264F9C");

                entity.ToTable("Студенты");

                entity.HasOne(d => d.КодГруппыNavigation)
                    .WithMany(p => p.Студентыs)
                    .HasForeignKey(d => d.КодГруппы)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Студенты__КодГру__6FE99F9F");

                entity.HasOne(d => d.КодПользователяNavigation)
                    .WithMany(p => p.Студентыs)
                    .HasForeignKey(d => d.КодПользователя)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Студенты__КодПол__6EF57B66");
            });

            modelBuilder.Entity<УчастникиСобытий>(entity =>
            {
                entity.HasKey(e => e.КодУчастника)
                    .HasName("PK__Участник__1D4507D0D8174F9C");

                entity.ToTable("УчастникиСобытий");

                entity.HasOne(d => d.КодПользователяNavigation)
                    .WithMany(p => p.УчастникиСобытийs)
                    .HasForeignKey(d => d.КодПользователя)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Участники__КодПо__0C85DE4D");

                entity.HasOne(d => d.КодСобытияNavigation)
                    .WithMany(p => p.УчастникиСобытийs)
                    .HasForeignKey(d => d.КодСобытия)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Участники__КодСо__0D7A0286");
            });

            modelBuilder.Entity<Факультеты>(entity =>
            {
                entity.HasKey(e => e.КодФакультета)
                    .HasName("PK__Факульте__7E28CC4CAAE2088C");

                entity.ToTable("Факультеты");

                entity.HasIndex(e => e.НазваниеФакультета, "UQ__Факульте__58EF202699661899")
                    .IsUnique();

                entity.Property(e => e.Декан).HasMaxLength(100);

                entity.Property(e => e.НазваниеФакультета).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
