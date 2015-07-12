using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class CategoryRepository : AbstractRepository<Category>
    {
        public CategoryRepository(TaskListContext context)
            : base(context)
        {
        }
    }
}
