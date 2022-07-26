using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hospi.App.Domain.Entities;
using Hospi.App.Persistence.AppRepositories;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Hospi.App.Frontend.Pages.RelativePage
{
    public class DeleteModel : PageModel
    {
        private readonly IRelativeRepository relativeRepository;
        private readonly IPatientRepository patientRepository;

        public DeleteModel(IServiceProvider serviceProvider)
        {
            this.relativeRepository = new RelativeRepository(new MyAppContext(serviceProvider.GetRequiredService<DbContextOptions<MyAppContext>>()));
            this.patientRepository = new PatientRepository(new MyAppContext(serviceProvider.GetRequiredService<DbContextOptions<MyAppContext>>()));
        }

        [BindProperty]
        public Relative Relative { get; set; }
        public Patient Patient { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Relative = await relativeRepository.Get(id);

            Patient = await patientRepository.Get(p => p.Relative.Id == id);

            if (Relative == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Relative = await relativeRepository.Get(id);

            if (Relative != null)
            {
                relativeRepository.Delete(Relative);
            }
            return RedirectToPage("./Index");
        }
    }
}
