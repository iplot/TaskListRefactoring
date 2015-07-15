using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Repositories;
using TaskList.Models;
using TaskListRefactoring.Infrastructure;
using TaskListRefactoring.Services;

namespace TaskListRefactoring.Controllers
{
    public class HomeController : Controller
    {
        private BasicService<Category> _categoryManager;
        private BasicService<Task> _taskManager;
        private BasicService<SubTask> _subTaskManager;

        public HomeController(BasicService<Category> categoryService, BasicService<Task> taskService,
            BasicService<SubTask> subTaskService)
        {
            _categoryManager = categoryService;
            _taskManager = taskService;
            _subTaskManager = subTaskService;
        }

        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            var result = _categoryManager.GetAllData();

            if (result.Success == null)
            {
                return View("ErrorPage", result.Errors);
            }

            return View(_categoryManager.GetAllData().Success);
        }

        [HttpPut]
        public ActionResult SaveTasks(List<TaskSaveViewModel> saveData)
        {
            if (saveData == null)
            {
                return new EmptyResult();
            }

            _taskManager.UpdateFinished(saveData.Where(s => s.TaskType == 0));
            _subTaskManager.UpdateFinished(saveData.Where(s => s.TaskType != 0));

            return Json("");
        }

        [HttpDelete]
        public ActionResult DeleteTasks(List<TaskSaveViewModel> deleteData)
        {
            if (deleteData == null)
            {
                return new EmptyResult();
            }

            _taskManager.DeleteEntity(deleteData.Where(s => s.TaskType == 0).Select(s => s.Id).ToArray());
            _subTaskManager.DeleteEntity(deleteData.Where(s => s.TaskType != 0).Select(s => s.Id).ToArray());

            return Json("Ok");
        }
    }
}