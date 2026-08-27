using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maincipito.Web.Pages.Relatives;

[Authorize]
public class DeleteModel(
    IRelativeRepository relativeRepository,
    IPatientRepository patientRepository) : PageModel
{
    [BindProperty]
    public Relative Relative { get; set; } = new();
    public Patient? Patient { get; set; }

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
        Patient = await patientRepository.GetAsync(p => p.Relative != null && p.Relative.Id == id.Value, cancellationToken);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id, CancellationToken cancellationToken)
    {
        if (id is null)
        {
            return NotFound();
        }

        await relativeRepository.DeleteAsync(id.Value, cancellationToken);

        return RedirectToPage("./Index");
    }
}