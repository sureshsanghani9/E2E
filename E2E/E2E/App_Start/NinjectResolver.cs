using E2ERepositories;
using E2ERepositories.Interface;
using Ninject;
using System;
using System.Collections.Generic;


namespace E2E.App_Start
{
    public class NinjectResolver : System.Web.Mvc.IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            this._kernel.Bind<IUserRepository>().To<UserRepository>();   
            this._kernel.Bind<ISubscriptionRepository>().To<SubscriptionRepository>();     
            this._kernel.Bind<IBusinessRepository>().To<BusinessRepository>();  
            this._kernel.Bind<ITaskRepository>().To<TaskRepository>();  
        }
    }
}