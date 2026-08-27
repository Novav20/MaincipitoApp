using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Maincipito.Web.Pages.Relatives;

[Authorize]
public class EditModel(
    IRelativeRepository relativeRepository,
    IPatientRepository patientRepository) : PageModel
{
    [BindProperty]
    public Relative Relative { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? id, CancellationToken cancellationToken)
    {
        if (id is null)
        {
            return NotFound();
        }

        var relative = await relativeRepository.GetByIdAsync(id.Value, cancellationToken);
        if (relative is null)
        {
            return NotFound();
        }

        Relative = relative;
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
            await relativeRepository.UpdateAsync(Relative, cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await relativeRepository.ExistsAsync(Relative.Id, cancellationToken))
            {
                return NotFound();
            }
            throw;
        }

        var patient = await patientRepository.GetAsync(p => p.Relative != null && p.Relative.Id == Relative.Id, cancellationToken);
        if (patient is not null)
        {
            return RedirectToPage("/Patients/Details", new { id = patient.Id });
        }

        return RedirectToPage("./Index");
    }
}