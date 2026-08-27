using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Maincipito.Persistence.Repositories;

public class PatientRepository(MaincipitoDbContext context) : Repository<Patient>(context), IPatientRepository
{
    public async Task<Patient?> GetWithDetailsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(p => p.Doctor)
            .Include(p => p.Relative)
            .Include(p => p.Nurse)
            .Include(p => p.History)
                .ThenInclude(h => h!.Suggestions)
            .Include(p => p.VitalSigns)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Patient>> GetAllAsync(string? searchString, CancellationToken cancellationToken = default)
    {
        IQueryable<Patient> query = DbSet.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            query = query.Where(p => p.Name.Contains(searchString) || p.Surname.Contains(searchString));
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Doctor?> AssignDoctorAsync(int patientId, int doctorId, CancellationToken cancellationToken = default)
    {
        var patient = await DbSet.FindAsync([patientId], cancellationToken);
        if (patient is null) return null;

        var doctor = await Context.Doctors.FindAsync([doctorId], cancellationToken);
        if (doctor is null) return null;

        patient.Doctor = doctor;
        await Context.SaveChangesAsync(cancellationToken);
        return doctor;
    }

    public async Task<Relative?> AssignRelativeAsync(int patientId, Relative relative, CancellationToken cancellationToken = default)
    {
        var patient = await DbSet.FindAsync([patientId], cancellationToken);
        if (patient is null) return null;

        patient.Relative = relative;
        await Context.SaveChangesAsync(cancellationToken);
        return relative;
    }

    public async Task<History?> AssignHistoryAsync(int patientId, History history, CancellationToken cancellationToken = default)
    {
        var patient = await DbSet.FindAsync([patientId], cancellationToken);
        if (patient is null) return null;

        patient.History = history;
        await Context.SaveChangesAsync(cancellationToken);
        return history;
    }

    public async Task<VitalSign?> AssignVitalSignAsync(int patientId, VitalSign vitalSign, CancellationToken cancellationToken = default)
    {
        var patient = await DbSet
            .Include(p => p.VitalSigns)
            .FirstOrDefaultAsync(p => p.Id == patientId, cancellationToken);

        if (patient is null) return null;

        patient.VitalSigns.Add(vitalSign);
        await Context.SaveChangesAsync(cancellationToken);
        return vitalSign;
    }
}