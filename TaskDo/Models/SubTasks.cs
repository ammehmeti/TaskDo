using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskDo.Models
{
    public class SubTasks
    {
        public int Id { get; set; }
        public string SubTaskName { get; set; }
        public string SubTaskDescription { get; set; }
        public DateTime SubTaskDate { get; set; }
        public int TaskId { get; set; }
    }
}