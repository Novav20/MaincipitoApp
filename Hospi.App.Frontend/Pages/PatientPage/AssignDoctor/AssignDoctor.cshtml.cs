using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospi.App.Domain.Entities;
using Hospi.App.Persistence.AppRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hospi.App.Frontend.Pages.PatientPage
{
    public class AssignDoctorModel : PageModel
    {
        private readonly IPatientRepository patientRepository;
        private readonly IDoctorRepository doctorRepository;


        public AssignDoctorModel(IServiceProvider serviceProvider)
        {
            this.patientRepository = new PatientRepository(
                new MyAppContext(serviceProvider
                .GetRequiredService<DbContextOptions<MyAppContext>>()));
            this.doctorRepository = new DoctorRepository(
                new MyAppContext(serviceProvider
                .GetRequiredService<DbContextOptions<MyAppContext>>()));

        }

        [BindProperty]
        public int? doctorId { get; set; }
        public int? patientId { get; set; }
        public Patient Patient { get; set; }
        public IList<Doctor> Doctors { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Doctors = await doctorRepository.GetAllDoctorsOrByName();

            Patient = patientRepository.Get(id);

            if (Patient == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost(int patientId, int doctorId)
        {
            patientRepository.AssignDoctor(patientId,doctorId);
            
            return RedirectToPage("../Index");
        }
    }
}
