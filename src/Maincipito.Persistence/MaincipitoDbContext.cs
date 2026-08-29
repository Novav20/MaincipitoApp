using Maincipito.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Maincipito.Persistence;

public class MaincipitoDbContext(DbContextOptions<MaincipitoDbContext> options) : DbContext(options)
{
    public DbSet<Person> People => Set<Person>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Nurse> Nurses => Set<Nurse>();
    public DbSet<Relative> Relatives => Set<Relative>();
    public DbSet<VitalSign> VitalSigns => Set<VitalSign>();
    public DbSet<History> Histories => Set<History>();
    public DbSet<CareSuggestion> CareSuggestions => Set<CareSuggestion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Mapeo de Herencia TPH
        modelBuilder.Entity<Person>()
            .ToTable("People");

        // 2. Borrado en Cascada sobre tablas hijas independientes
        modelBuilder.Entity<Patient>()
            .HasMany(p => p.VitalSigns)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<History>()
            .HasMany(h => h.Suggestions)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        // 3. Relación 1-a-1 Patient -> History (SetNull para evitar cascadas circulares)
        modelBuilder.Entity<Patient>()
            .HasOne(p => p.History)
            .WithOne()
            .HasForeignKey<Patient>("HistoryId")
            .OnDelete(DeleteBehavior.SetNull);

        // 4. Relaciones autorreferenciales en tabla People (Restrict / No Action para SQL Server 1785)
        modelBuilder.Entity<Patient>()
            .HasOne(p => p.Doctor)
            .WithMany(d => d.Patients)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Patient>()
            .HasOne(p => p.Relative)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Patient>()
            .HasOne(p => p.Nurse)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
    }
}