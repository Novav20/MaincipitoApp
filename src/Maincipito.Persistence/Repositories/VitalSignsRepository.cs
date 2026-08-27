using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Maincipito.Persistence.Repositories;

public class VitalSignRepository(MaincipitoDbContext context) : Repository<VitalSign>(context), IVitalSignRepository
{
    public async Task<IReadOnlyList<VitalSign>> GetAllOrByTypeAsync(Sign? sign = null, CancellationToken cancellationToken = default)
    {
        IQueryable<VitalSign> query = DbSet.AsNoTracking();

        if (sign.HasValue)
        {
            query = query.Where(v => v.Sign == sign.Value);
        }

        return await query.ToListAsync(cancellationToken);
    }
}