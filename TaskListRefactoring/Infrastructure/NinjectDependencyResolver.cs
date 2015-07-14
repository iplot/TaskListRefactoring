using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Repositories;
using Ninject;
using TaskListRefactoring.Services;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

namespace TaskListRefactoring.Infrastructure
{
    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver, System.Web.Http.Dependencies.IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel) : base(kernel)
        {
            _kernel = kernel;
            addBindings();
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(_kernel.BeginBlock());
        }

        private void addBindings()
        {
            _kernel.Bind<AbstractRepository<Category>>().To<CategoryRepository>();
            _kernel.Bind<AbstractRepository<Task>>().To<TaskRepository>();
            _kernel.Bind<AbstractRepository<SubTask>>().To<SubTaskRepository>(); 
            
            _kernel.Bind<BasicService<Category>>().To<CategoryManager>();
            _kernel.Bind<BasicService<Task>>().To<TaskManager>();
            _kernel.Bind<BasicService<SubTask>>().To<SubTaskManager>();
        }
    }
}