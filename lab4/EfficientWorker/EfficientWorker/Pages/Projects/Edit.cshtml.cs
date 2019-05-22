using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EfficientWorker.Models;

namespace EfficientWorker.Pages.Projects
{
    public class EditModel : PageModel
    {
        private readonly EfficientWorker.Models.EfficientWorkerContext _context;

        public EditModel(EfficientWorker.Models.EfficientWorkerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EfficientWorker.Models.Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Project
                .Include(p => p.Worker).FirstOrDefaultAsync(m => m.ProjectID == id);

            if (Project == null)
            {
                return NotFound();
            }
           ViewData["WorkerID"] = new SelectList(_context.Worker, "ID", "ID");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(Project.ProjectID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.ProjectID == id);
        }
    }
}
