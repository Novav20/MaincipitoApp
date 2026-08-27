using Maincipito.Domain.Entities;

namespace Maincipito.Domain.Repositories;

public interface IHistoryRepository : IRepository<History>
{
    Task<History?> GetWithSuggestionsAsync(int id, CancellationToken cancellationToken = default);
    Task<CareSuggestion?> AssignCareSuggestionAsync(int historyId, CareSuggestion careSuggestion, CancellationToken cancellationToken = default);
}