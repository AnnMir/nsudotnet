using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EfficientWorker.Models
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
       
        public int ProjectCost { get; set; }

        public int? WorkerID { get; set; }
        public Worker Worker { get; set; }
    }
}
