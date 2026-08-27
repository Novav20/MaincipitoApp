using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maincipito.Web.Pages.VitalSigns;

[Authorize]
public class IndexModel(IVitalSignRepository vitalSignRepository) : PageModel
{
    public IReadOnlyList<VitalSign> VitalSigns { get; set; } = [];

    [BindProperty(SupportsGet = true)]
    public Sign? SignFilter { get; set; }

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        VitalSigns = await vitalSignRepository.GetAllOrByTypeAsync(SignFilter, cancellationToken);
    }
}