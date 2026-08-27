using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maincipito.Web.Pages.Histories;

[Authorize]
public class CreateModel(
    IHistoryRepository historyRepository,
    IPatientRepository patientRepository) : PageModel
{
    [BindProperty]
    public History History { get; set; } = new();

    public Patient? Patient { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? PatientId { get; set; }

    public async Task<IActionResult> OnGetAsync(int? patientId, CancellationToken cancellationToken)
    {
        if (patientId.HasValue)
        {
            Patient = await patientRepository.GetByIdAsync(patientId.Value, cancellationToken);
            PatientId = patientId.Value;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (PatientId.HasValue)
        {
            var createdHistory = await patientRepository.AssignHistoryAsync(PatientId.Value, History, cancellationToken);
            if (createdHistory is not null)
            {
                return RedirectToPage("./Details", new { id = createdHistory.Id });
            }
        }

        await historyRepository.AddAsync(History, cancellationToken);
        return RedirectToPage("./Index");
    }
}