using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maincipito.Web.Pages.Histories.CareSuggestions;

[Authorize]
public class IndexModel(IRepository<CareSuggestion> careSuggestionRepository) : PageModel
{
    public IReadOnlyList<CareSuggestion> CareSuggestions { get; set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        CareSuggestions = await careSuggestionRepository.GetAllAsync(cancellationToken);
    }
}