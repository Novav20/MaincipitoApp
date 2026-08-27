using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maincipito.Web.Pages.Doctors;

[Authorize]
public class IndexModel(IDoctorRepository doctorRepository) : PageModel
{
    public IReadOnlyList<Doctor> Doctors { get; set; } = [];

    [BindProperty(SupportsGet = true)]
    public string? SearchString { get; set; }

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Doctors = await doctorRepository.GetAllAsync(SearchString, cancellationToken);
    }
}