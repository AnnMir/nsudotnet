using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfficientWorker.Models
{
    public class Worker
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
