using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hospi.App.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hospi.App.Persistence.AppRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace Hospi.App.Frontend.Pages.PatientPage
{
    public class IndexModel : PageModel
    {
        private readonly IPatientRepository patientRepository;

        public IndexModel(IServiceProvider serviceProvider)
        {
            this.patientRepository = new PatientRepository(new MyAppContext(serviceProvider.GetRequiredService<DbContextOptions<MyAppContext>>()));
        }

        public IList<Patient> Patients { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Names { get; set; }
        public async Task OnGetAsync()
        {
            Patients = await patientRepository.GetAllPatientsOrByName(SearchString);
        }
    }
}
