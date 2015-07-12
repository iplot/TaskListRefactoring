using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace TaskListRefactoring.Services
{
    public class CategoryManager : BasicService<Category>
    {
        public CategoryManager(AbstractRepository<Category> repositry) : base(repositry)
        {
        }
    }
}