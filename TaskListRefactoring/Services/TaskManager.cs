using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Entities;
using DataAccess.Repositories;
using TaskList.Models;

namespace TaskListRefactoring.Services
{
    public class TaskManager : BasicService<Task>
    {
        public TaskManager(AbstractRepository<Task> repositry) : base(repositry)
        {
        }

        public override ServiceResult GetAllData()
        {
            var result = base.GetAllData();

            if (result.Success == null)
            {
                return result;
            }

            var tasks = (List<Task>) result.Success;
            for (int i = 0; i < tasks.Count; i++)
            {
                tasks[i].SubTasks = null;
                tasks[i].Category = null;
            }

            result.Success = tasks;

            return result;
        }

        public ServiceResult GetTasksByCategory(int categoryId)
        {
            try
            {
                var tasks = (List<Task>) GetAllData().Success;
                var categoryTasks = tasks.Where(t => t.CategoryId == categoryId).ToList();

                return new ServiceResult {Errors = "", Success = categoryTasks};
            }
            catch (Exception exception)
            {
                return new ServiceResult {Errors = exception.Message, Success = null};
            }
        }
    }
}