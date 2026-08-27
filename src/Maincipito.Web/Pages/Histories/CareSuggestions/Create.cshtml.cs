using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maincipito.Web.Pages.Histories.CareSuggestions;

[Authorize]
public class CreateModel(
    IHistoryRepository historyRepository,
    IPatientRepository patientRepository) : PageModel
{
    public History History { get; set; } = new();
    public Patient? Patient { get; set; }

    [BindProperty]
    public CareSuggestion CareSuggestion { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? historyId, CancellationToken cancellationToken)
    {
        if (historyId is null)
        {
            return NotFound();
        }

        var history = await historyRepository.GetWithSuggestionsAsync(historyId.Value, cancellationToken);
        if (history is null)
        {
            return NotFound();
        }

        History = history;
        Patient = await patientRepository.GetAsync(p => p.History != null && p.History.Id == historyId.Value, cancellationToken);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int historyId, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var history = await historyRepository.GetWithSuggestionsAsync(historyId, cancellationToken);
            if (history is not null)
            {
                History = history;
                Patient = await patientRepository.GetAsync(p => p.History != null && p.History.Id == historyId, cancellationToken);
            }
            return Page();
        }

        await historyRepository.AssignCareSuggestionAsync(historyId, CareSuggestion, cancellationToken);
        return RedirectToPage("/Histories/Details", new { id = historyId });
    }
}