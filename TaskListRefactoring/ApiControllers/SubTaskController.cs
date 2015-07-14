using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Repositories;
using TaskListRefactoring.Services;

namespace TaskListRefactoring.ApiControllers
{
    public class SubTaskController : ApiController
    {
        private BasicService<Category> _categoryManager;
        private BasicService<Task> _taskManager;
        private BasicService<SubTask> _subTaskManager;

        public SubTaskController(BasicService<Category> categoryService, BasicService<Task> taskService,
            BasicService<SubTask> subTaskService)
        {
            _categoryManager = categoryService;
            _taskManager = taskService;
            _subTaskManager = subTaskService;
        }

        [HttpPost]
        [Route("subtask/add", Name = "AddSubtask")]
        public SubTask AddSubTask(SubTask subTask)
        {
            var result = _subTaskManager.AddEntity(subTask);

            if (result.Success != null)
            {
                return result.Success as SubTask;
            }

            return null;
        }
    }
}
