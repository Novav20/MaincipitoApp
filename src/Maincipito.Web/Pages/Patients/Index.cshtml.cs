using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maincipito.Web.Pages.Patients;

[Authorize]
public class IndexModel(IPatientRepository patientRepository) : PageModel
{
    public IReadOnlyList<Patient> Patients { get; set; } = [];

    [BindProperty(SupportsGet = true)]
    public string? SearchString { get; set; }

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Patients = await patientRepository.GetAllAsync(SearchString, cancellationToken);
    }
}