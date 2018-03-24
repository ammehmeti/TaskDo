using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskDo.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime TaskDate { get; set; }

    }
}