using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hospi.App.Domain.Entities;
using Hospi.App.Persistence.AppRepositories;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

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

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Patient = await patientRepository.Get(id);

            if (Patient == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Patient = await patientRepository.Get(id);

            if (Patient != null)
            {
                patientRepository.Delete(Patient);
            }
            return RedirectToPage("./Index");
        }
    }
}
