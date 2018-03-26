using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskDo.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime TaskDate { get; set; }
        public int ListId { get; set; }
        public int Completed { get; set; }

    }
}