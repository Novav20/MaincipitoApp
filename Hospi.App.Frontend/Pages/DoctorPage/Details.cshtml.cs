using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hospi.App.Domain.Entities;
using Hospi.App.Persistence.AppRepositories;

namespace Hospi.App.Frontend.Pages.DoctorPage
{
    public class DetailsModel : PageModel
    {
        private readonly Hospi.App.Persistence.AppRepositories.MyAppContext _context;

        public DetailsModel(Hospi.App.Persistence.AppRepositories.MyAppContext context)
        {
            _context = context;
        }

        public Doctor Doctor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Doctor = await _context.Doctors.FirstOrDefaultAsync(m => m.Id == id);

            if (Doctor == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
