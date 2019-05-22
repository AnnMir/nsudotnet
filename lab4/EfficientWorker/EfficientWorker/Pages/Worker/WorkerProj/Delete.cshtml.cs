using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EfficientWorker.Models;

namespace EfficientWorker.Pages.Worker.WorkerProj
{
    public class DeleteModel : PageModel
    {
        private readonly EfficientWorker.Models.EfficientWorkerContext _context;

        public DeleteModel(EfficientWorker.Models.EfficientWorkerContext context)
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
                .Include(p => p.Worker).Where(p => p.Worker.ID == id).FirstOrDefaultAsync(m => m.ProjectID == projid);

            if (Project == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, int? projid)
        {
            if (id == null || projid == null)
            {
                return NotFound();
            }

            Project = await _context.Project.FindAsync(projid);
            
            if (Project != null)
            {
                _context.Project.Remove(Project);
                await _context.SaveChangesAsync();
            }

            Project.WorkerID = null;

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
                /*if (!ProjectExists(Project.ProjectID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }*/
            }

            return RedirectToPage("/Worker/Index");
        }

        /*private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.ProjectID == id);
        }*/
    }
}
