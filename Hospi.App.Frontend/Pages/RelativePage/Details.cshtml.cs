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

namespace Hospi.App.Frontend.Pages.RelativePage
{
    public class DetailsModel : PageModel
    {
        private readonly IRelativeRepository relativeRepository;

        public DetailsModel(IServiceProvider serviceProvider)
        {
            this.relativeRepository = new RelativeRepository(new MyAppContext(serviceProvider.GetRequiredService<DbContextOptions<MyAppContext>>()));

        }

        public Relative Relative { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Relative = await relativeRepository.Get(id);

            if (Relative == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
