using Maincipito.Domain.Entities;

namespace Maincipito.Domain.Repositories;

public interface INurseRepository : IRepository<Nurse>
{
    Task<IReadOnlyList<Nurse>> GetAllAsync(string? searchString, CancellationToken cancellationToken = default);
}