using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maincipito.Web.Pages.Histories;

[Authorize]
public class DeleteModel(IHistoryRepository historyRepository) : PageModel
{
    [BindProperty]
    public History History { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int? id, CancellationToken cancellationToken)
    {
        if (id is null)
        {
            return NotFound();
        }

        var history = await historyRepository.GetByIdAsync(id.Value, cancellationToken);
        if (history is null)
        {
            return NotFound();
        }

        History = history;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id, CancellationToken cancellationToken)
    {
        if (id is null)
        {
            return NotFound();
        }

        await historyRepository.DeleteAsync(id.Value, cancellationToken);

        return RedirectToPage("./Index");
    }
}