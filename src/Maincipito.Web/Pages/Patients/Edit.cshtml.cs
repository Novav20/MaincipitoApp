using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Maincipito.Web.Pages.Patients;

[Authorize]
public class EditModel(IPatientRepository patientRepository) : PageModel
{
    [BindProperty]
    public Patient Patient { get; set; } = new();

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
            await patientRepository.UpdateAsync(Patient, cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await patientRepository.ExistsAsync(Patient.Id, cancellationToken))
            {
                return NotFound();
            }
            throw;
        }

        return RedirectToPage("./Details", new { id = Patient.Id });
    }
}