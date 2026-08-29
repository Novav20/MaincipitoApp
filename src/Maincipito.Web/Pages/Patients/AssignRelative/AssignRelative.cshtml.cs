using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maincipito.Web.Pages.Patients.AssignRelative;

[Authorize]
public class AssignRelativeModel(
    IPatientRepository patientRepository,
    IRelativeRepository relativeRepository) : PageModel
{
    public Patient Patient { get; set; } = new();
    public IReadOnlyList<Relative> Relatives { get; set; } = [];

    [BindProperty]
    public int RelativeId { get; set; }

    [BindProperty]
    public int PatientId { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id, CancellationToken cancellationToken)
    {
        if (id is null)
        {
            return NotFound();
        }

        var patient = await patientRepository.GetByIdAsync(id.Value, cancellationToken);
        if (patient is null)
        {
            return NotFound();
        }

        Patient = patient;
        Relatives = await relativeRepository.GetAllAsync(null, cancellationToken);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        await patientRepository.AssignRelativeAsync(PatientId, RelativeId, cancellationToken);
        return RedirectToPage("/Patients/Details", new { id = PatientId });
    }
}