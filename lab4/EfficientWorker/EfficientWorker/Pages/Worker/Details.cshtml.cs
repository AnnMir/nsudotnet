using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EfficientWorker.Models;
using Remotion.Linq.Clauses;

namespace EfficientWorker.Pages.Worker
{
    public class DetailsModel : PageModel
    {
        private readonly EfficientWorker.Models.EfficientWorkerContext _context;

        public DetailsModel(EfficientWorker.Models.EfficientWorkerContext context)
        {
            _context = context;
        }

        public EfficientWorker.Models.Worker Worker { get; set; }
        public IList<EfficientWorker.Models.Project> Projects1 { get; set; }
        public IList<EfficientWorker.Models.Project> Projects { get; set; }
        public IList<EfficientWorker.Models.Worker> Workers { get; set; }

        public async Task OnGetAsync(int? id)
        {
            IQueryable<EfficientWorker.Models.Worker> workerIQ = from p in _context.Worker where p.ID==id
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

            Workers = await workerIQ.ToListAsync();
            //Projects = workerIQ.   .Projects.ToList();
            //Worker = await _context.Worker.FirstOrDefaultAsync(m => m.ID == id);
            //Projects = Worker.Projects.ToList();
            //var proj = _context.Project.Where(s => s.WorkerID == id);
            //Projects = await proj.ToListAsync();
        }
    }
}

/*
        public async Task OnGetAsync()
        {
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
}*/
