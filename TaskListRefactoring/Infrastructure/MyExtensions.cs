using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Entities;
using TaskList.Models;
using TaskListRefactoring.Services;

namespace TaskListRefactoring.Infrastructure
{
    public static class MyExtensions
    {
        public static ServiceResult GetTasksByCategoryId(this BasicService<Task> manager, int categoryId)
        {
            var result = manager.GetAllData();

            if (result.Success != null)
            {
                result.Success = ((List<Task>) result.Success).Where(t => t.CategoryId == categoryId).ToList();
            }

            return result;
        }

        public static void UpdateFinished(this BasicService<Task> manager, IEnumerable<TaskSaveViewModel> saveData)
        {
            try
            {
                var entities =
                    ((List<Task>) manager.GetAllData().Success).Where(t => saveData.Any(s => s.Id == t.TaskId));
                
                foreach (var entity in entities)
                {
                    entity.IsFinished = saveData.Where(s => s.Id == entity.TaskId).Select(s => s.IsFinished).First();
                    manager.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                
            }
        }

        public static void UpdateFinished(this BasicService<SubTask> manager, IEnumerable<TaskSaveViewModel> saveData)
        {
            try
            {
                var entities =
                    ((List<SubTask>)manager.GetAllData().Success).Where(t => saveData.Any(s => s.Id == t.SubTaskId));

                foreach (var entity in entities)
                {
                    entity.IsFinished = saveData.Where(s => s.Id == entity.SubTaskId).Select(s => s.IsFinished).First();
                    manager.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {

            }
        }
    }
}