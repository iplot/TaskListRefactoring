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

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            var result = _categoryManager.AddEntity(category);

            if (result.Success == null)
            {
                return View("ErrorPage", result.Success);
            }

            return Json(result.Success);
        }

        [HttpGet]
        public ActionResult GetTasks(int categoryId)
        {
            var result = _taskManager.GetTasksByCategoryId(categoryId);

            if (result.Success == null)
            {
                return View(result.Errors);
            }

            var response = (List<Task>)result.Success;
            (_subTaskManager as SubTaskManager).AddSubtasksToTasks(response);

            return PartialView(response);
        }

        [HttpPost]
        public ActionResult AddTask(Task task)
        {
            if (task.CategoryId == 0)
            {
                return RedirectToAction("Index");
            }

            var result = _taskManager.AddEntity(task);

            if (result.Success == null)
            {
                return View("ErrorPage", result.Errors);
            }

            task = result.Success as Task;
            task.SubTasks = new List<SubTask>();

            return PartialView("Task", task);
        }

        [HttpPost]
        public ActionResult AddSubtask(SubTask subtask)
        {
            var result = _subTaskManager.AddEntity(subtask);

            if (result.Success != null)
            {
                var newSubTask = result.Success as SubTask;
                return Json(
                    new
                    {
                        TaskId = newSubTask.TaskId,
                        Text = newSubTask.Text,
                        IsFinished = newSubTask.IsFinished,
                        SubTaskId = newSubTask.SubTaskId
                    });
            }
            else
            {
                return View("ErrorPage", result.Errors);
            }
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