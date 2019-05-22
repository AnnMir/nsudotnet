using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EfficientWorker.Models
{
    public class EfficientWorkerContext : DbContext
    {
        public EfficientWorkerContext(DbContextOptions<EfficientWorkerContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Worker> Worker { get; set; }
        public DbSet<Project> Project { get; set; }
    }
}
