using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maincipito.Web.Pages.Doctors;

[Authorize]
public class CreateModel(IDoctorRepository doctorRepository) : PageModel
{
    [BindProperty]
    public Doctor Doctor { get; set; } = new();

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

        await doctorRepository.AddAsync(Doctor, cancellationToken);

        return RedirectToPage("./Index");
    }
}