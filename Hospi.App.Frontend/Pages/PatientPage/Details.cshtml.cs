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
        public DetailsModel(IServiceProvider serviceProvider)
        {
            this.patientRepository = new PatientRepository(
                new MyAppContext(serviceProvider
                .GetRequiredService<DbContextOptions<MyAppContext>>()));
        }
        public Patient Patient { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Patient = await patientRepository.Get(id);

            return Page();
        }
    }
}
