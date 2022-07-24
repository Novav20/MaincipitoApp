using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hospi.App.Domain.Entities;
using Hospi.App.Persistence.AppRepositories;

namespace Hospi.App.Frontend.Pages.RelativePage
{
    public class IndexModel : PageModel
    {
        private readonly Hospi.App.Persistence.AppRepositories.MyAppContext _context;

        public IndexModel(Hospi.App.Persistence.AppRepositories.MyAppContext context)
        {
            _context = context;
        }

        public IList<Relative> Relative { get;set; }

        public async Task OnGetAsync()
        {
            Relative = await _context.Relatives.ToListAsync();
        }
    }
}
