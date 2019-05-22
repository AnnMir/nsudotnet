using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EfficientWorker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace EfficientWorker.Pages.Worker.WorkerProj
{
    public class CreateModel : PageModel
    {
        private readonly EfficientWorker.Models.EfficientWorkerContext _context;

        public CreateModel(EfficientWorker.Models.EfficientWorkerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Worker = await _context.Worker.FirstOrDefaultAsync(m => m.ID == id);

            if (Worker == null)
            {
                return NotFound();
            }
            int[] numbers = { Worker.ID};
            ViewData["ProjectName"] = new SelectList(_context.Project, "ProjectName", "ProjectName");
            ViewData["ProjectCost"] = new SelectList(_context.Project, "ProjectCost", "ProjectCost");
            ViewData["WorkerId"] = new SelectList(numbers);
            return Page();
        }

        [BindProperty]
        public EfficientWorker.Models.Project Project { get; set; }

        public EfficientWorker.Models.Worker Worker { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Project.Add(Project);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Worker/Index");
        }
    }
}