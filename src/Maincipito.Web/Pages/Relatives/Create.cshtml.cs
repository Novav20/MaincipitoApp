using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maincipito.Web.Pages.Relatives;

[Authorize]
public class CreateModel(
    IRelativeRepository relativeRepository,
    IPatientRepository patientRepository) : PageModel
{
    [BindProperty]
    public Relative Relative { get; set; } = new();

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
            await patientRepository.AssignRelativeAsync(PatientId.Value, Relative, cancellationToken);
            return RedirectToPage("/Patients/Details", new { id = PatientId.Value });
        }

        await relativeRepository.AddAsync(Relative, cancellationToken);
        return RedirectToPage("./Index");
    }
}