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

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Doctor = doctorRepository.Get(id);

            if (Doctor == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Doctor = doctorRepository.Get(id);
            //Doctor =  _context.Doctors.FindAsync(id);

            if (Doctor != null)
            {
                //_context.Doctors.Remove(Doctor);
                //await _context.SaveChangesAsync();
                doctorRepository.Delete(Doctor);
            }
            return RedirectToPage("./Index");
        }
    }
}
