using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EfficientWorker.Models;

namespace EfficientWorker.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly EfficientWorker.Models.EfficientWorkerContext _context;

        public IndexModel(EfficientWorker.Models.EfficientWorkerContext context)
        {
            _context = context;
        }
        public IList<EfficientWorker.Models.Project> Project { get; set; }
        public async Task OnGet()
        {
            Project = await _context.Project
                .Include(p => p.Worker).ToListAsync();
        }
    }
}