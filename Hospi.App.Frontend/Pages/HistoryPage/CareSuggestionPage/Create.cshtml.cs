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

namespace Hospi.App.Frontend.Pages.HistoryPage.CareSuggestionPage
{
    public class CreateModel : PageModel
    {
        private readonly IHistoryRepository historyRepository;
        private readonly IPatientRepository patientRepository;
        public CreateModel(IServiceProvider serviceProvider)
        {
            this.historyRepository = new HistoryRepository(
                new MyAppContext(serviceProvider
                .GetRequiredService<DbContextOptions<MyAppContext>>()));
            this.patientRepository = new PatientRepository(
                new MyAppContext(serviceProvider
                .GetRequiredService<DbContextOptions<MyAppContext>>()));
        }

        [BindProperty(SupportsGet = true)]
        public History History { get; set; }
        public CareSuggestion CareSuggestion { get; set; }
        public Patient Patient { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnGetAsync(int id)
        {
            History = await historyRepository.Get(id);

            Patient = await patientRepository.Get(p => p.History.Id == id);

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id, CareSuggestion careSuggestion)
        {
            if (!ModelState.IsValid)
            {
                History = await historyRepository.Get(id);
                Patient = await patientRepository.Get(p => p.History.Id == id);

                return Page();
            }
            CareSuggestion = await historyRepository.AssignCareSuggestion(id, careSuggestion);

            if (CareSuggestion != null)
            {
                return RedirectToPage("../Details", new { id = id });
            }
            return Page();


        }
    }
}
