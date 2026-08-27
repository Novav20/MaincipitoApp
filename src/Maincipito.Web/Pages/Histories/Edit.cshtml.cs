using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Maincipito.Web.Pages.Histories;

[Authorize]
public class EditModel(
    IHistoryRepository historyRepository,
    IPatientRepository patientRepository) : PageModel
{
    [BindProperty]
    public History History { get; set; } = new();
    public Patient? Patient { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id, CancellationToken cancellationToken)
    {
        if (id is null)
        {
            return NotFound();
        }

        var history = await historyRepository.GetByIdAsync(id.Value, cancellationToken);
        if (history is null)
        {
            return NotFound();
        }

        History = history;
        Patient = await patientRepository.GetAsync(p => p.History != null && p.History.Id == id.Value, cancellationToken);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            await historyRepository.UpdateAsync(History, cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await historyRepository.ExistsAsync(History.Id, cancellationToken))
            {
                return NotFound();
            }
            throw;
        }

        return RedirectToPage("./Details", new { id = History.Id });
    }
}