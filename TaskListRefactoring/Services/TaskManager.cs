using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Entities;
using DataAccess.Repositories;
using TaskList.Models;

namespace TaskListRefactoring.Services
{
    public class TaskManager : BasicService<Task>, IFinishedChanhgeable
    {
        public TaskManager(AbstractRepository<Task> repositry) : base(repositry)
        {
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