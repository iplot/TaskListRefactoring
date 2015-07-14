using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class SubTaskRepository : AbstractRepository<SubTask>
    {
        public SubTaskRepository()
            : base()
        {
        }
    }
}
