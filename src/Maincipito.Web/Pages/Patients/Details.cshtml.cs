using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maincipito.Web.Pages.Patients;

[Authorize]
public class DetailsModel(IPatientRepository patientRepository) : PageModel
{
    public Patient Patient { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? id, CancellationToken cancellationToken)
    {
        if (id is null)
        {
            return NotFound();
        }

        var patient = await patientRepository.GetWithDetailsAsync(id.Value, cancellationToken);
        if (patient is null)
        {
            return NotFound();
        }

        Patient = patient;
        return Page();
    }
}