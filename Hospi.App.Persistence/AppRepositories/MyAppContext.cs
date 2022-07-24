using Hospi.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospi.App.Persistence.AppRepositories
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options): base(options)
        {
        }

        public DbSet<Person> People { get; set; } 
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Relative> Relatives { get; set; }
        public DbSet<VitalSign> VitalSigns { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<CareSuggestion> CareSuggestions { get; set; }
    }
}