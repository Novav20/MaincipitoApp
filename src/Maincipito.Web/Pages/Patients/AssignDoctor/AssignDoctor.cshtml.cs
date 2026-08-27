using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maincipito.Web.Pages.Patients.AssignDoctor;

[Authorize]
public class AssignDoctorModel(
    IPatientRepository patientRepository,
    IDoctorRepository doctorRepository) : PageModel
{
    public Patient Patient { get; set; } = new();
    public IReadOnlyList<Doctor> Doctors { get; set; } = [];

    [BindProperty]
    public int DoctorId { get; set; }

    [BindProperty]
    public int PatientId { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id, CancellationToken cancellationToken)
    {
        if (id is null)
        {
            return NotFound();
        }

        var patient = await patientRepository.GetByIdAsync(id.Value, cancellationToken);
        if (patient is null)
        {
            return NotFound();
        }

        Patient = patient;
        Doctors = await doctorRepository.GetAllAsync(null, cancellationToken);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        await patientRepository.AssignDoctorAsync(PatientId, DoctorId, cancellationToken);
        return RedirectToPage("/Patients/Details", new { id = PatientId });
    }
}