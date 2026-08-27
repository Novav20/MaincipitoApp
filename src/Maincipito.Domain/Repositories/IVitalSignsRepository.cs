using Maincipito.Domain.Entities;

namespace Maincipito.Domain.Repositories;

public interface IVitalSignRepository : IRepository<VitalSign>
{
    Task<IReadOnlyList<VitalSign>> GetAllOrByTypeAsync(Sign? sign = null, CancellationToken cancellationToken = default);
}