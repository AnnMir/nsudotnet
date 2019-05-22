using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EfficientWorker.Models;

namespace EfficientWorker.Pages.Worker
{
    public class DeleteModel : PageModel
    {
        private readonly EfficientWorker.Models.EfficientWorkerContext _context;

        public DeleteModel(EfficientWorker.Models.EfficientWorkerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EfficientWorker.Models.Worker Worker { get; set; }

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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Worker = await _context.Worker.FindAsync(id);

            if (Worker != null)
            {
                _context.Worker.Remove(Worker);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
