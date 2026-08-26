using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Hospi.App.Frontend.Pages.PatientPage
{
    public class SeeHistoryModel : PageModel
    {
        private readonly ILogger<SeeHistoryModel> _logger;

        public SeeHistoryModel(ILogger<SeeHistoryModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}