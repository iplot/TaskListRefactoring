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

//        public object GetService(Type serviceType)
//        {
//            return _kernel.TryGet(serviceType);
//        }
//
//        public IEnumerable<object> GetServices(Type serviceType)
//        {
//            return _kernel.GetAll(serviceType);
//        }

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