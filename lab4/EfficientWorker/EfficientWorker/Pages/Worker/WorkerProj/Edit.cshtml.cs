using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EfficientWorker.Models;

namespace EfficientWorker.Pages.Worker.WorkerProj
{
    public class EditModel : PageModel
    {
        private readonly EfficientWorker.Models.EfficientWorkerContext _context;

        public EditModel(EfficientWorker.Models.EfficientWorkerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? projid)
        {
            if (id == null || projid == null)
            {
                return NotFound();
            }

            Project = await _context.Project
                .Include(p => p.Worker).Where(p => p.WorkerID == id).FirstOrDefaultAsync(m => m.ProjectID == projid);

            if (Project == null)
            {
                return NotFound();
            }

            int workerID = Project.WorkerID.Value;
            int[] numbers = { workerID };
            ViewData["WorkerID"] = new SelectList(numbers);
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

            return RedirectToPage("/Worker/Index");
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.ProjectID == id);
        }
    }
}
