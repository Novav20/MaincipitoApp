using Maincipito.Domain.Entities;

namespace Maincipito.Domain.Repositories;

public interface IRelativeRepository : IRepository<Relative>
{
    Task<IReadOnlyList<Relative>> GetAllAsync(string? searchString, CancellationToken cancellationToken = default);
}