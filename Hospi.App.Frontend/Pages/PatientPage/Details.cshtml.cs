using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hospi.App.Domain.Entities;
using Hospi.App.Persistence.AppRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace Hospi.App.Frontend.Pages.PatientPage
{
    public class DetailsModel : PageModel
    {
        private readonly IPatientRepository patientRepository;
        private readonly IDoctorRepository doctorRepository;
        private readonly IRelativeRepository relativeRepository;

        private readonly Hospi.App.Persistence.AppRepositories.MyAppContext _context;
        public DetailsModel(IServiceProvider serviceProvider, MyAppContext context)
        {
            this.patientRepository = new PatientRepository(
                new MyAppContext(serviceProvider
                .GetRequiredService<DbContextOptions<MyAppContext>>()));

            this.doctorRepository = new DoctorRepository(
                new MyAppContext(serviceProvider
                .GetRequiredService<DbContextOptions<MyAppContext>>()));

            this.relativeRepository = new RelativeRepository(
                new MyAppContext(serviceProvider
                .GetRequiredService<DbContextOptions<MyAppContext>>()));

            _context = context;
        }

        public Patient Patient { get; set; }

        public Doctor Doctor { get; set; }
        public Relative Relative { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string query = "SELECT * FROM People WHERE Id = {0}";
            Patient = await _context.Patients
       .FromSqlRaw(query, id)
       .Include(d => d.Doctor)
       .Include(d => d.Relative)
       .AsNoTracking()
       .FirstOrDefaultAsync();

            Doctor = Patient.Doctor;

            Relative = Patient.Relative;

            return Page();
        }
    }
}
