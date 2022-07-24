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
    public class DeleteModel : PageModel
    {
        private readonly IPatientRepository patientRepository;

        public DeleteModel(IServiceProvider serviceProvider)
        {
            this.patientRepository = new PatientRepository(new MyAppContext(serviceProvider.GetRequiredService<DbContextOptions<MyAppContext>>()));
        }

        [BindProperty]
        public Patient Patient { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Patient = patientRepository.Get(id);

            if (Patient == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Patient = patientRepository.Get(id);
            //Patient =  _context.Patients.FindAsync(id);

            if (Patient != null)
            {
                //_context.Patients.Remove(Patient);
                //await _context.SaveChangesAsync();
                patientRepository.Delete(Patient);
            }
            return RedirectToPage("./Index");
        }
    }
}
