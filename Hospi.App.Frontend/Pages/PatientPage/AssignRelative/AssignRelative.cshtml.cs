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
    public class AssignRelativeModel : PageModel
    {
        private readonly IPatientRepository patientRepository;
        private readonly IRelativeRepository relativeRepository;


        public AssignRelativeModel(IServiceProvider serviceProvider)
        {
            this.patientRepository = new PatientRepository(
                new MyAppContext(serviceProvider
                .GetRequiredService<DbContextOptions<MyAppContext>>()));
            this.relativeRepository = new RelativeRepository(
                new MyAppContext(serviceProvider
                .GetRequiredService<DbContextOptions<MyAppContext>>()));

        }

        [BindProperty]
        public int? relativeId { get; set; }
        public int? patientId { get; set; }
        public Patient Patient { get; set; }
        public IList<Relative> Relatives { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Relatives = await relativeRepository.GetAllRelativesOrByName();

            Patient = patientRepository.Get(id);

            if (Patient == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost(int patientId, int relativeId)
        {
            var AssignedRelative = patientRepository.AssignRelative(patientId, relativeId);

            return RedirectToPage("../Index");
        }
    }
}
