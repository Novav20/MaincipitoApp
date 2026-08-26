using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hospi.App.Domain.Entities;
using Hospi.App.Persistence.AppRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hospi.App.Frontend.Pages.DoctorPage
{
    public class CreateModel : PageModel
    {
        private readonly IDoctorRepository doctorRepository;

        public CreateModel(IServiceProvider serviceProvider)
        {
            this.doctorRepository = new DoctorRepository(new MyAppContext(serviceProvider.GetRequiredService<DbContextOptions<MyAppContext>>()));
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Doctor Doctor { get; set; }



        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Doctors.Add(Doctor);
            //await _context.SaveChangesAsync();
            doctorRepository.Add(Doctor);


            return RedirectToPage("./Index");
        }
    }
}
