using Maincipito.Domain.Entities;

namespace Maincipito.Domain.Repositories;

public interface IDoctorRepository : IRepository<Doctor>
{
    Task<Doctor?> GetWithPatientsAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Doctor>> GetAllAsync(string? searchString, CancellationToken cancellationToken = default);
}