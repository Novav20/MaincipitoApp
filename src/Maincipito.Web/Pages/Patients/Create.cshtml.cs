using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maincipito.Web.Pages.Patients;

[Authorize]
public class CreateModel(IPatientRepository patientRepository) : PageModel
{
    [BindProperty]
    public Patient Patient { get; set; } = new();

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await patientRepository.AddAsync(Patient, cancellationToken);

        return RedirectToPage("./Index");
    }
}