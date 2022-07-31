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

namespace Hospi.App.Frontend.Pages.HistoryPage
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

        [BindProperty]
        public History History { get; set; }
        public Patient Patient { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            History = await patientRepository.AssignHistory(id, History);

            return RedirectToPage("./Details",new {id = History.Id});
        }
    }
}
