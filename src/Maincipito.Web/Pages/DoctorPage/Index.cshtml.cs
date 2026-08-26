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
    public class IndexModel : PageModel
    {
        private readonly Hospi.App.Persistence.AppRepositories.MyAppContext _context;

        public IndexModel(Hospi.App.Persistence.AppRepositories.MyAppContext context)
        {
            _context = context;
        }

        public IList<Doctor> Doctor { get;set; }

        public async Task OnGetAsync()
        {
            Doctor = await _context.Doctors.ToListAsync();
        }
    }
}
