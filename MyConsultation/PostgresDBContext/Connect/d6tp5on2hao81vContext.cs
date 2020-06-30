using Microsoft.EntityFrameworkCore;
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
        public virtual DbSet<Сonsultation> Сonsultation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
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
