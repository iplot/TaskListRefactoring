using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class TaskRepository : AbstractRepository<Task>
    {
        public TaskRepository(TaskListContext context)
            : base(context)
        {
        }
    }
}
