using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskDo.Models;

namespace TaskDo.TasksHelper
{
    public class TaskHelper
    {
        private static  string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TaskDo"].ToString();
        public static void AddTask(Tasks task)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
               
                task.TaskDate = DateTime.Now ;
                string query = @"Insert into Tasks(TaskName,TaskDescription,TaskDate,ListId,Completed) values (@taskName,@taskDescription,@taskDate,@listId,@completed)";
                SqlCommand cmd = new SqlCommand(query,con);
                cmd.Parameters.AddWithValue("@taskName",task.TaskName);
                cmd.Parameters.AddWithValue("@taskDescription", task.TaskDescription);
                cmd.Parameters.AddWithValue("@taskDate", task.TaskDate);
                cmd.Parameters.AddWithValue("@listId",task.ListId);
                cmd.Parameters.AddWithValue("@completed",task.Completed);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }
        public static List<Tasks> ReadTask()
        {
            List<Tasks> tasks = new List<Tasks>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"Select Id,TaskName,TaskDescription,TaskDate,Completed from Tasks";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Tasks taski = new Tasks();

                       

                        taski.Id = Convert.ToInt32(reader["Id"]);
                        taski.TaskName = reader["TaskName"].ToString();
                        taski.TaskDescription = reader["TaskDescription"].ToString();
                        taski.TaskDate = Convert.ToDateTime(reader["TaskDate"]);
                        taski.Completed = Convert.ToInt32(reader["Completed"]);
                        tasks.Add(taski);

                    }
                }
                    
                con.Close();
            }
            return tasks;

        }
        public static Tasks GetTaskToEdit(int taskId)
        {
            Tasks task = new Tasks();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"Select Id,TaskName,TaskDescription,TaskDate from Tasks where Id = @taskId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@taskId", taskId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        task.Id = Convert.ToInt32(reader["Id"]);
                        task.TaskName = reader["TaskName"].ToString();
                        task.TaskDescription = reader["TaskDescription"].ToString();
                        task.TaskDate = Convert.ToDateTime(reader["TaskDate"]);
                    }
                }
                con.Close();
            }
            return task;
        }
        public static void EditTask(Tasks taskToEdit)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                taskToEdit.TaskDate = DateTime.Now;
                string query = @"Update Tasks set TaskName = @taskName, TaskDescription = @taskDescription, TaskDate = @taskDate where Id = @taskId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@taskName", taskToEdit.TaskName);
                cmd.Parameters.AddWithValue("@taskDescription", taskToEdit.TaskDescription);
                cmd.Parameters.AddWithValue("@taskDate", taskToEdit.TaskDate);
                cmd.Parameters.AddWithValue("@taskId", taskToEdit.Id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public static void DeleteTask(int taskId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"Delete from Tasks where Id = @taskId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@taskId", taskId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public static List<SubTasks> getSubTasks(int taskId)
        {
            List<SubTasks> subTasks = new List<SubTasks>();
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"Select Id,SubTaskName,SubTaskDescription,SubTaskDate,TaskId from SubTasks where TaskId =  @taskId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@taskId",taskId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SubTasks subtaski = new SubTasks();
                        subtaski.Id = Convert.ToInt32(reader["Id"]);
                        subtaski.SubTaskName = reader["SubTaskName"].ToString();
                        subtaski.SubTaskDescription = reader["SubTaskDescription"].ToString();
                        subtaski.SubTaskDate = Convert.ToDateTime(reader["SubTaskDate"]);
                        subtaski.TaskId = Convert.ToInt32(reader["TaskId"]);
                        subTasks.Add(subtaski);

                    }
                }

                con.Close();
            }
            return subTasks;
        }
        public static bool AddSubTask(SubTasks subTask)
        {
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                subTask.SubTaskDate = DateTime.Now;
                string query = @"Insert into SubTasks(SubTaskName,SubTaskDescription,SubTaskDate,TaskId) values (@subtaskName,@subtaskDescription,@subtaskDate,@taskId)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@subtaskName", subTask.SubTaskName);
                cmd.Parameters.AddWithValue("@subtaskDescription", subTask.SubTaskDescription);
                cmd.Parameters.AddWithValue("@subtaskDate", subTask.SubTaskDate);
                cmd.Parameters.AddWithValue("@taskId", subTask.TaskId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }
        public static bool DeleteSubTask(int subTaskId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"Delete from SubTasks where Id = @subtaskId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@subtaskId", subTaskId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }
        public static List<Lists> getLists()
        {
            List<Lists> listat = new List<Lists>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"Select Id,ListName,ListDesc from Lists";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Lists lista = new Lists();
                        lista.Id = Convert.ToInt32(reader["Id"]);
                        lista.ListName = reader["ListName"].ToString();
                        lista.ListDesc= reader["ListDesc"].ToString();

                        listat.Add(lista);

                    }
                }

                con.Close();
            }
            return listat;
        }
        public static List<Tasks> getListTasks(int listId)
        {
            List<Tasks> tasks = new List<Tasks>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"Select Id,TaskName,TaskDescription,TaskDate,ListId from Tasks where ListId = @listId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@listId",listId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Tasks taski = new Tasks();
                        taski.Id = Convert.ToInt32(reader["Id"]);
                        taski.TaskName = reader["TaskName"].ToString();
                        taski.TaskDescription = reader["TaskDescription"].ToString();
                        taski.TaskDate = Convert.ToDateTime(reader["TaskDate"]);
                        taski.ListId = Convert.ToInt32(reader["ListId"]);
                        tasks.Add(taski);

                    }
                }

                con.Close();
            }
            return tasks;
        }
        public static void AddList(Lists list)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                
                string query = @"Insert into Lists(ListName,ListDesc) values (@listName,@listDesc)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@listName", list.ListName);
                cmd.Parameters.AddWithValue("@listDesc", list.ListDesc);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }
        public static Lists getListtoEdit(int listId)
        {
            Lists lista = new Lists();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"Select Id,ListName,ListDesc from Lists where Id = @listId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@listId", listId);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Id = Convert.ToInt32(reader["Id"]);
                        lista.ListName = reader["ListName"].ToString();
                        lista.ListDesc = reader["ListDesc"].ToString();
                    }
                }
                con.Close();
            }
            return lista;
        }
        public static void DeleteList(int listId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"Delete from Lists where Id = @listId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@listId", listId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public static List<SelectListItem> getSelectList()
        {
            List<SelectListItem> selectlistItem = new List<SelectListItem>();
            List<Lists> listat = getLists();
            foreach (var item in listat)
            {
                selectlistItem.Add(new SelectListItem() { Text= item.ListName, Value= item.Id.ToString()});
            }
            
            return selectlistItem;
        }
        public static bool AssignTask(int taskId,int listId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"Update Tasks set ListId = @listId where Id = @taskId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@taskId", taskId);
                cmd.Parameters.AddWithValue("@listId", listId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }
        public static bool MarkTask(int taskId, int check)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"Update Tasks set Completed = @check where Id = @taskId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@taskId", taskId);
                cmd.Parameters.AddWithValue("@check", check);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }
    }
}