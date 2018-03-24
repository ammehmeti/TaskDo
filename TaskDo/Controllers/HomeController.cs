using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskDo.Models;

namespace TaskDo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View(TasksHelper.TaskHelper.ReadTask());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult AddTask(Tasks task)
        {

            TasksHelper.TaskHelper.AddTask(task);

            return RedirectToAction("Index");
        }
        public ActionResult EditTask(int taskId)
        {
            Tasks task = TasksHelper.TaskHelper.GetTaskToEdit(taskId);
             
            return View("_editTask",task);
        }
        [HttpPost]
        public ActionResult EditTask(Tasks task)
        {
            TasksHelper.TaskHelper.EditTask(task);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteTask(int taskId)
        {
            TasksHelper.TaskHelper.DeleteTask(taskId);
            return RedirectToAction("Index");
        }
        public ActionResult SubTasks(int taskId)
        {
            List<SubTasks> subTasks = TasksHelper.TaskHelper.getSubTasks(taskId);
            ViewBag.TaskName = TasksHelper.TaskHelper.GetTaskToEdit(taskId).TaskName;
            return View(subTasks);
        }
        [HttpPost]
        public ActionResult AddSubTasks(SubTasks subTask)
        {
            TasksHelper.TaskHelper.AddSubTask(subTask);
            return RedirectToAction("SubTasks",new { taskId = subTask.TaskId });
        }
        public ActionResult DeleteSubTask(int subTaskId , int taskId)
        {
            TasksHelper.TaskHelper.DeleteSubTask(subTaskId);
            return RedirectToAction("SubTasks",new {taskId = taskId });
        }
    }
}