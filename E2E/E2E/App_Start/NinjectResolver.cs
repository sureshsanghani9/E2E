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
            _kernel.Bind<IUserRepository>().To<UserRepository>();   
            _kernel.Bind<ISubscriptionRepository>().To<SubscriptionRepository>();     
            _kernel.Bind<IBusinessRepository>().To<BusinessRepository>();  
            _kernel.Bind<ITaskRepository>().To<TaskRepository>();  
            _kernel.Bind<IReportRepository>().To<ReportRepository>();  
        }
    }
}