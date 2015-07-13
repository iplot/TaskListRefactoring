using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.UI.WebControls;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Repositories;
using TaskList.Models;
using TaskListRefactoring.Infrastructure;
using TaskListRefactoring.Services;

namespace TaskListRefactoring.ApiControllers
{
    public class TaskController : ApiController
    {
        private BasicService<Category> _categoryManager;
        private BasicService<Task> _taskManager;
        private BasicService<SubTask> _subTaskManager;

        public TaskController(BasicService<Category> categoryService, BasicService<Task> taskService,
            BasicService<SubTask> subTaskService)
        {
            _categoryManager = categoryService;
            _taskManager = taskService;
            _subTaskManager = subTaskService;
        }

        [System.Web.Http.HttpGet]
        public IEnumerable<Task> GetTasks()
        {
            var result = _taskManager.GetAllData();

            if (result.Success == null)
            {
                return new List<Task>();
            }

            var response = (List<Task>) result.Success;

            return response;
        }

        [System.Web.Http.HttpPost]
        public Task AddTask(Task task)
        {
            if (task.CategoryId == 0)
            {
                return null;
            }

            var result = _taskManager.AddEntity(task);

            if (result.Success == null)
            {
                return null;
            }

            return result.Success as Task;;
        }

        [HttpPut]
        public void SaveTasks(List<TaskSaveViewModel> saveData)
        {
            if (saveData == null)
            {
                return;
            }

            _taskManager.UpdateFinished(saveData.Where(s => s.TaskType == 0));
            _subTaskManager.UpdateFinished(saveData.Where(s => s.TaskType != 0));
        }

        [HttpDelete]
        public string DeleteTasks(List<TaskSaveViewModel> deleteData)
        {
            if (deleteData == null)
            {
                return "";
            }

            _taskManager.DeleteEntity(deleteData.Where(s => s.TaskType == 0).Select(s => s.Id).ToArray());
            _subTaskManager.DeleteEntity(deleteData.Where(s => s.TaskType != 0).Select(s => s.Id).ToArray());

            return "Ok";
        }
    }
}
