using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maincipito.Web.Pages.Histories;

[Authorize]
public class IndexModel(IHistoryRepository historyRepository) : PageModel
{
    public IReadOnlyList<History> Histories { get; set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Histories = await historyRepository.GetAllAsync(cancellationToken);
    }
}