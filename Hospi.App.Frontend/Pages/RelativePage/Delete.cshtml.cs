using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hospi.App.Domain.Entities;
using Hospi.App.Persistence.AppRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace Hospi.App.Frontend.Pages.RelativePage
{
    public class DeleteModel : PageModel
    {
        private readonly IRelativeRepository relativeRepository;

        public DeleteModel(IServiceProvider serviceProvider)
        {
            this.relativeRepository = new RelativeRepository(new MyAppContext(serviceProvider.GetRequiredService<DbContextOptions<MyAppContext>>()));
        }

        [BindProperty]
        public Relative Relative { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Relative = relativeRepository.Get(id);

            if (Relative == null)
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

            Relative = relativeRepository.Get(id);

            if (Relative != null)
            {
                relativeRepository.Delete(Relative);
            }
            return RedirectToPage("./Index");
        }
    }
}
