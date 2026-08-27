using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Maincipito.Web.Pages.VitalSigns;

[Authorize]
public class EditModel(IVitalSignRepository vitalSignRepository) : PageModel
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

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            await vitalSignRepository.UpdateAsync(VitalSign, cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await vitalSignRepository.ExistsAsync(VitalSign.Id, cancellationToken))
            {
                return NotFound();
            }
            throw;
        }

        return RedirectToPage("./Index");
    }
}