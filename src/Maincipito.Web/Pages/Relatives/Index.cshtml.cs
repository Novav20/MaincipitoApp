using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maincipito.Web.Pages.Relatives;

[Authorize]
public class IndexModel(IRelativeRepository relativeRepository) : PageModel
{
    public IReadOnlyList<Relative> Relatives { get; set; } = [];

    [BindProperty(SupportsGet = true)]
    public string? SearchString { get; set; }

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Relatives = await relativeRepository.GetAllAsync(SearchString, cancellationToken);
    }
}