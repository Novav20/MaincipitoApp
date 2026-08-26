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

namespace Hospi.App.Frontend.Pages.DoctorPage
{
    public class DeleteModel : PageModel
    {
        private readonly IDoctorRepository doctorRepository;

        public DeleteModel(IServiceProvider serviceProvider)
        {
            this.doctorRepository = new DoctorRepository(new MyAppContext(serviceProvider.GetRequiredService<DbContextOptions<MyAppContext>>()));
        }

        [BindProperty]
        public Doctor Doctor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Doctor = await doctorRepository.Get(id);

            if (Doctor == null)
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
            Doctor = await doctorRepository.Get(id);

            if (Doctor != null)
            {
                doctorRepository.Delete(Doctor);
            }
            return RedirectToPage("./Index");
        }
    }
}
