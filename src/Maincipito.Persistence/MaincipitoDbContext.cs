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

        // Configuración de herencia TPH (Table-Per-Hierarchy) limpia para Person
        modelBuilder.Entity<Person>()
            .ToTable("People");
    }
}