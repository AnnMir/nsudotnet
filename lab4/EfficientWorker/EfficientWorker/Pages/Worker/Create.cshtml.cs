using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EfficientWorker.Models;

namespace EfficientWorker.Pages.Worker
{
    public class CreateModel : PageModel
    {
        private readonly EfficientWorker.Models.EfficientWorkerContext _context;

        public CreateModel(EfficientWorker.Models.EfficientWorkerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public EfficientWorker.Models.Worker Worker { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Worker.Add(Worker);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}