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

namespace Hospi.App.Frontend.Pages.HistoryPage
{
    public class DetailsModel : PageModel
    {
        private readonly IHistoryRepository historyRepository;
        private readonly IPatientRepository patientRepository;

        public DetailsModel(IServiceProvider serviceProvider)
        {
            this.historyRepository = new HistoryRepository(
                new MyAppContext(serviceProvider
                .GetRequiredService<DbContextOptions<MyAppContext>>()));
            this.patientRepository = new PatientRepository(
                new MyAppContext(serviceProvider
                .GetRequiredService<DbContextOptions<MyAppContext>>()));
        }
        [BindProperty]
        public History History { get; set; }
        public Patient Patient {get;set;}
        
        [BindProperty(Name = "id", SupportsGet = true)]
        public int id { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            History = await historyRepository.Get(id);

            Patient = await patientRepository.Get(p => p.History.Id == id);

            if (History == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
