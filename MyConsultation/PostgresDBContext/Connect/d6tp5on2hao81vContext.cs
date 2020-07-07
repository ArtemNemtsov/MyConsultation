using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DBContext.Models;

namespace DBContext.Connect
{
    public partial class d6tp5on2hao81vContext : DbContext
    {
        public d6tp5on2hao81vContext()
        {
        }

        public d6tp5on2hao81vContext(DbContextOptions<d6tp5on2hao81vContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<VJournalConsultation> VJournalConsultation { get; set; }
        public virtual DbSet<VJournalPatient> VJournalPatient { get; set; }
        public virtual DbSet<Сonsultation> Сonsultation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.IdPatient)
                    .HasName("Patient_pkey");

                entity.ToTable("Patient", "ref");

                entity.HasIndex(e => e.Snils)
                    .HasName("Patient_snils_key")
                    .IsUnique();

                entity.Property(e => e.IdPatient).HasColumnName("id_patient");

                entity.Property(e => e.Birthdate)
                    .HasColumnName("birthdate")
                    .HasColumnType("date")
                    .HasComment("дата рождения");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasMaxLength(1)
                    .HasComment("пол");

                entity.Property(e => e.MiddleName)
                    .HasColumnName("middle_name")
                    .HasMaxLength(30)
                    .HasComment("отчество");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30)
                    .HasComment("имя");

                entity.Property(e => e.Snils)
                    .IsRequired()
                    .HasColumnName("snils")
                    .HasMaxLength(14)
                    .HasComment("снилс");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(30)
                    .HasComment("фамилия");
            });

            modelBuilder.Entity<VJournalConsultation>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("v_journal_consultation", "order");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.FioPatient).HasColumnName("fio_patient");

                entity.Property(e => e.IdConsultation).HasColumnName("id_consultation");

                entity.Property(e => e.Symptoms).HasColumnName("symptoms");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("time(0) without time zone");
            });

            modelBuilder.Entity<VJournalPatient>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("v_journal_patient", "ref");

                entity.Property(e => e.Birthdate)
                    .HasColumnName("birthdate")
                    .HasColumnType("date");

                entity.Property(e => e.Fio).HasColumnName("fio");

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(1);

                entity.Property(e => e.IdPatient).HasColumnName("id_patient");

                entity.Property(e => e.Snils)
                    .HasColumnName("snils")
                    .HasMaxLength(14);
            });

            modelBuilder.Entity<Сonsultation>(entity =>
            {
                entity.HasKey(e => e.IdConsultation)
                    .HasName("consultation_pkey");

                entity.ToTable("Сonsultation", "order");

                entity.Property(e => e.IdConsultation)
                    .HasColumnName("id_consultation")
                    .HasDefaultValueSql("nextval('\"order\".consultation_id_consultation_seq'::regclass)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("now()")
                    .HasComment("дата консультации");

                entity.Property(e => e.IdPatient)
                    .HasColumnName("id_patient")
                    .HasComment("id пациента");

                entity.Property(e => e.Symptoms)
                    .HasColumnName("symptoms")
                    .HasComment("симптомы");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("time(0) without time zone")
                    .HasDefaultValueSql("now()")
                    .HasComment("время консультанции");

                entity.HasOne(d => d.IdPatientNavigation)
                    .WithMany(p => p.Сonsultation)
                    .HasForeignKey(d => d.IdPatient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("consultation_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
