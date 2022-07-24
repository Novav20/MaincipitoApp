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

namespace Hospi.App.Frontend.Pages.RelativePage
{
    public class CreateModel : PageModel
    {
        private readonly IRelativeRepository relativeRepository;

        public CreateModel(IServiceProvider serviceProvider)
        {
            this.relativeRepository = new RelativeRepository(new MyAppContext(serviceProvider.GetRequiredService<DbContextOptions<MyAppContext>>()));
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Relative Relative { get; set; }
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            relativeRepository.Add(Relative);
            
            return RedirectToPage("./Index");
        }
    }
}
