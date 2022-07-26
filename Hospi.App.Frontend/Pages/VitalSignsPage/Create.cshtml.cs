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

namespace Hospi.App.Frontend.Pages.VitalSignsPage
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

        [BindProperty]
        public VitalSign VitalSign { get; set; }
        public Patient Patient { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Patient = await patientRepository.Get(id);
            return Page();
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            VitalSign = await patientRepository.AssignVitalSign(id,VitalSign);

            return RedirectToPage("../PatientPage/Details", new {id = id});
        }
    }
}
