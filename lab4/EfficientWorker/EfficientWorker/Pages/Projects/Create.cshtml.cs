using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EfficientWorker.Models;

namespace EfficientWorker.Pages.Projects
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
        ViewData["WorkerID"] = new SelectList(_context.Worker, "ID", "LastName");
            return Page();
        }

        [BindProperty]
        public EfficientWorker.Models.Project Project { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Project.Add(Project);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}