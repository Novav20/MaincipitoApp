using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Maincipito.Web.Pages.Doctors;

[Authorize]
public class EditModel(IDoctorRepository doctorRepository) : PageModel
{
    [BindProperty]
    public Doctor Doctor { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? id, CancellationToken cancellationToken)
    {
        if (id is null)
        {
            return NotFound();
        }

        var doctor = await doctorRepository.GetByIdAsync(id.Value, cancellationToken);
        if (doctor is null)
        {
            return NotFound();
        }

        Doctor = doctor;
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
            await doctorRepository.UpdateAsync(Doctor, cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await doctorRepository.ExistsAsync(Doctor.Id, cancellationToken))
            {
                return NotFound();
            }
            throw;
        }

        return RedirectToPage("./Index");
    }
}