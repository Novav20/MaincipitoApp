using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Maincipito.Persistence.Repositories;

public class HistoryRepository(MaincipitoDbContext context) : Repository<History>(context), IHistoryRepository
{
    public async Task<History?> GetWithSuggestionsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(h => h.Suggestions)
            .AsNoTracking()
            .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
    }

    public async Task<CareSuggestion?> AssignCareSuggestionAsync(int historyId, CareSuggestion careSuggestion, CancellationToken cancellationToken = default)
    {
        var history = await DbSet
            .Include(h => h.Suggestions)
            .FirstOrDefaultAsync(h => h.Id == historyId, cancellationToken);

        if (history is null) return null;

        careSuggestion.DateTime = DateTime.UtcNow;
        history.Suggestions.Add(careSuggestion);
        await Context.SaveChangesAsync(cancellationToken);

        return careSuggestion;
    }
}