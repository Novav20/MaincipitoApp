using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hospi.App.Domain.Entities;
using Hospi.App.Persistence.AppRepositories;

namespace Hospi.App.Frontend.Pages.HistoryPage
{
    public class DeleteModel : PageModel
    {
        private readonly Hospi.App.Persistence.AppRepositories.MyAppContext _context;

        public DeleteModel(Hospi.App.Persistence.AppRepositories.MyAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public History History { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            History = await _context.Histories.FirstOrDefaultAsync(m => m.Id == id);

            if (History == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            History = await _context.Histories.FindAsync(id);

            if (History != null)
            {
                _context.Histories.Remove(History);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
