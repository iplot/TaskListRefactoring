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
    public class CategoryController : ApiController
    {
        private BasicService<Category> _categoryManager;
        private BasicService<Task> _taskManager;
        private BasicService<SubTask> _subTaskManager;

        public CategoryController(BasicService<Category> categoryService, BasicService<Task> taskService,
            BasicService<SubTask> subTaskService)
        {
            _categoryManager = categoryService;
            _taskManager = taskService;
            _subTaskManager = subTaskService;
        }

        [HttpGet]
        public IEnumerable<Category> GetCategories()
        {
            var result = _categoryManager.GetAllData();

            if (result.Success != null)
            {
                return (List<Category>)result.Success;
            }

            return null;
        }

        [HttpPost]
        public Category AddCategory(Category category)
        {
            var result = _categoryManager.AddEntity(category);

            if (result.Success != null)
            {
                return result.Success as Category;
            }

            return null;
        }
    }
}
