using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Maincipito.Persistence.Repositories;

public class DoctorRepository(MaincipitoDbContext context) : Repository<Doctor>(context), IDoctorRepository
{
    public async Task<Doctor?> GetWithPatientsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(d => d.Patients)
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Doctor>> GetAllAsync(string? searchString, CancellationToken cancellationToken = default)
    {
        IQueryable<Doctor> query = DbSet.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            //TODO: OCP violated if new search criteria are added. Consider using Specification pattern or Expression<Func<Doctor, bool>> for more flexibility.
            query = query.Where(d => d.Name.Contains(searchString) || d.Surname.Contains(searchString) || d.MedicalSpecialty.Contains(searchString));
        }

        return await query.ToListAsync(cancellationToken);
    }
}