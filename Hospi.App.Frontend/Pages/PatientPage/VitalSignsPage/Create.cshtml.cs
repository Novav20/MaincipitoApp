using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hospi.App.Domain.Entities;
using Hospi.App.Persistence.AppRepositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Hospi.App.Frontend.Pages.PatientPage.VitalSignsPage
{
    public class CreateModel : PageModel
    {
        private readonly IPatientRepository patientRepository;

        public CreateModel(IServiceProvider serviceProvider)
        {
            this.patientRepository = new PatientRepository(
                new MyAppContext(serviceProvider
                .GetRequiredService<DbContextOptions<MyAppContext>>()));
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public VitalSign VitalSign { get; set; }
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            VitalSign = await patientRepository.AssignVitalSign(id,VitalSign);

            return RedirectToPage("../Details", new {id = id});
        }
    }
}
