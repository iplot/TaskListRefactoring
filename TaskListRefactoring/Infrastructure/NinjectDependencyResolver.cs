using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Repositories;
using Ninject;
using TaskListRefactoring.Services;

namespace TaskListRefactoring.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            addBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void addBindings()
        {
//            _kernel.Bind<DbContext>().To<TaskListContext>();

            _kernel.Bind<AbstractRepository<Category>>().To<CategoryRepository>()
                .WithConstructorArgument("context", new TaskListContext());
            _kernel.Bind<AbstractRepository<Task>>().To<TaskRepository>()
                .WithConstructorArgument("context", new TaskListContext());
            _kernel.Bind<AbstractRepository<SubTask>>().To<SubTaskRepository>()
                .WithConstructorArgument("context", new TaskListContext()); 
            
            _kernel.Bind<BasicService<Category>>().To<CategoryManager>();
            _kernel.Bind<BasicService<Task>>().To<TaskManager>();
            _kernel.Bind<BasicService<SubTask>>().To<SubTaskManager>();
        }
    }
}