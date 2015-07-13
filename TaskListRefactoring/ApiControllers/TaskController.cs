using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Repositories;
using TaskListRefactoring.Services;

namespace TaskListRefactoring.ApiControllers
{
    public class TaskController : ApiController
    {
        private BasicService<Category> _categoryManager;
        private BasicService<Task> _taskManager;
        private BasicService<SubTask> _subTaskManager;

//        public TaskController(BasicService<Category> categoryService, BasicService<Task> taskService,
//            BasicService<SubTask> subTaskService)
//        {
//            _categoryManager = categoryService;
//            _taskManager = taskService;
//            _subTaskManager = subTaskService;
//        }

        public TaskController()
        {
            TaskListContext context = new TaskListContext();

            AbstractRepository<Category> catrep = new CategoryRepository(context);
            AbstractRepository<Task> taskrep = new TaskRepository(context);
            AbstractRepository<SubTask> subtrep = new SubTaskRepository(context);

            _categoryManager = new CategoryManager(catrep);
            _taskManager = new TaskManager(taskrep);
            _subTaskManager = new SubTaskManager(subtrep);
        }

        [HttpGet]
        public IEnumerable<Task> GetTasks()
        {
//            var result = ((TaskManager)_taskManager).GetAllData();
            var result = _taskManager.GetAllData();

            if (result.Success == null)
            {
                return new List<Task>();
            }

            var response = (List<Task>) result.Success;

            return response;
        }
    }
}
