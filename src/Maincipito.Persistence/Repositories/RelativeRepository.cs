using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Maincipito.Persistence.Repositories;

public class RelativeRepository(MaincipitoDbContext context) : Repository<Relative>(context), IRelativeRepository
{
    public async Task<IReadOnlyList<Relative>> GetAllAsync(string? searchString, CancellationToken cancellationToken = default)
    {
        IQueryable<Relative> query = DbSet.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            query = query.Where(r => r.Name.Contains(searchString) 
                                  || r.Surname.Contains(searchString) 
                                  || r.Relationship.Contains(searchString) 
                                  || r.Email.Contains(searchString));
        }

        return await query.ToListAsync(cancellationToken);
    }
}