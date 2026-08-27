using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maincipito.Web.Pages.VitalSigns;

[Authorize]
public class CreateModel(
    IVitalSignRepository vitalSignRepository,
    IPatientRepository patientRepository) : PageModel
{
    [BindProperty]
    public VitalSign VitalSign { get; set; } = new();

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
            await patientRepository.AssignVitalSignAsync(PatientId.Value, VitalSign, cancellationToken);
            return RedirectToPage("/Patients/Details", new { id = PatientId.Value });
        }

        await vitalSignRepository.AddAsync(VitalSign, cancellationToken);
        return RedirectToPage("./Index");
    }
}