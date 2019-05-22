using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EfficientWorker.Models;

namespace EfficientWorker.Pages.Rept
{
    public class IndexModel : PageModel
    {
        private readonly EfficientWorker.Models.EfficientWorkerContext _context;

        public IndexModel(EfficientWorker.Models.EfficientWorkerContext context)
        {
            _context = context;
        }

        public EfficientWorker.Models.Worker Worker { get; set; }
        public IList<EfficientWorker.Models.Worker> Workers { get; set; }
        public IList<EfficientWorker.Models.Project> Projects { get; set; }

        public string CostSort { get; set; }

        public async Task OnGetAsync()
        {

            IQueryable<EfficientWorker.Models.Worker> workerIQ = from p in _context.Worker
                select p;

            Projects = await _context.Project.ToListAsync();

            foreach (var w in workerIQ)
            {
                foreach (var p in Projects)
                {
                    if (w.ID.Equals(p.WorkerID))
                        w.Projects.Add(p);
                }
            }

            workerIQ = workerIQ.OrderByDescending(s => s.Projects.Sum(p => p.ProjectCost));
            Workers = await workerIQ.ToListAsync();
        }
    }
}