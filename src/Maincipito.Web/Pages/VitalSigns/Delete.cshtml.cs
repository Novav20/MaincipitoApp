using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maincipito.Web.Pages.VitalSigns;

[Authorize]
public class DeleteModel(IVitalSignRepository vitalSignRepository) : PageModel
{
    [BindProperty]
    public VitalSign VitalSign { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? id, CancellationToken cancellationToken)
    {
        if (id is null)
        {
            return NotFound();
        }

        var vitalSign = await vitalSignRepository.GetByIdAsync(id.Value, cancellationToken);
        if (vitalSign is null)
        {
            return NotFound();
        }

        VitalSign = vitalSign;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id, CancellationToken cancellationToken)
    {
        if (id is null)
        {
            return NotFound();
        }

        await vitalSignRepository.DeleteAsync(id.Value, cancellationToken);

        return RedirectToPage("./Index");
    }
}