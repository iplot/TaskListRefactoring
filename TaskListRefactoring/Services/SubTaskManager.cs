using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Entities;
using DataAccess.Repositories;
using TaskList.Models;

namespace TaskListRefactoring.Services
{
    public class SubTaskManager : BasicService<SubTask>, IFinishedChanhgeable
    {
        public SubTaskManager(AbstractRepository<SubTask> repositry) : base(repositry)
        {
        }

        public ServiceResult GetSubtasksByTaskId(int taskId)
        {
            try
            {
                var subtasks = (List<SubTask>) GetAllData().Success;
                var subtasksByTask = subtasks.Where(s => s.TaskId == taskId).ToList();

                return new ServiceResult {Errors = "", Success = subtasksByTask};
            }
            catch (Exception exception)
            {
                return new ServiceResult {Errors = exception.Message, Success = null};
            }
        }

        public void AddSubtasksToTasks(IEnumerable<Task> tasks)
        {
            var subtasks = (List<SubTask>)GetAllData().Success;

            foreach (var task in tasks)
            {
                task.SubTasks = subtasks.Where(s => s.TaskId == task.TaskId).ToList();
            }
        }
    }
}