using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maincipito.Web.Pages.Doctors;

[Authorize]
public class DetailsModel(IDoctorRepository doctorRepository) : PageModel
{
    public Doctor Doctor { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? id, CancellationToken cancellationToken)
    {
        if (id is null)
        {
            return NotFound();
        }

        var doctor = await doctorRepository.GetWithPatientsAsync(id.Value, cancellationToken);
        if (doctor is null)
        {
            return NotFound();
        }

        Doctor = doctor;
        return Page();
    }
}