using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maincipito.Web.Pages.Histories;

[Authorize]
public class DetailsModel(
    IHistoryRepository historyRepository,
    IPatientRepository patientRepository) : PageModel
{
    public History History { get; set; } = new();
    public Patient? Patient { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id, CancellationToken cancellationToken)
    {
        if (id is null)
        {
            return NotFound();
        }

        var history = await historyRepository.GetWithSuggestionsAsync(id.Value, cancellationToken);
        if (history is null)
        {
            return NotFound();
        }

        History = history;
        Patient = await patientRepository.GetAsync(p => p.History != null && p.History.Id == id.Value, cancellationToken);

        return Page();
    }
}