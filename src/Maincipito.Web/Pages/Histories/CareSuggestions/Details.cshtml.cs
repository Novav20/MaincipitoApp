using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maincipito.Web.Pages.Histories.CareSuggestions;

[Authorize]
public class DetailsModel(IRepository<CareSuggestion> careSuggestionRepository) : PageModel
{
    public CareSuggestion CareSuggestion { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? id, CancellationToken cancellationToken)
    {
        if (id is null)
        {
            return NotFound();
        }

        var suggestion = await careSuggestionRepository.GetByIdAsync(id.Value, cancellationToken);
        if (suggestion is null)
        {
            return NotFound();
        }

        CareSuggestion = suggestion;
        return Page();
    }
}