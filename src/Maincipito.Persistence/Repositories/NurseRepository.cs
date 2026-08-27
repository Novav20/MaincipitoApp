using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Maincipito.Persistence.Repositories;

public class NurseRepository(MaincipitoDbContext context) : Repository<Nurse>(context), INurseRepository
{
    public async Task<IReadOnlyList<Nurse>> GetAllAsync(string? searchString, CancellationToken cancellationToken = default)
    {
        IQueryable<Nurse> query = DbSet.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            query = query.Where(n => n.Name.Contains(searchString) 
                                  || n.Surname.Contains(searchString) 
                                  || n.ProfessionalCard.Contains(searchString));
        }

        return await query.ToListAsync(cancellationToken);
    }
}