using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LoadBalancer.Controllers;
using Ninject;

namespace LoadBalancer.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
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
            //kernel.Bind<IRepository>().To<BookRepository>();
            /*RouteTableStorage routeTableStorage = new RouteTableStorage();
            _kernel.Bind<RouteTableStorage>().ToConstant(routeTableStorage);
            var routeTable = routeTableStorage.Load();
            _kernel.Bind<RouteTable>().ToConstant(routeTable);
            _kernel.Bind<AuthProvider>().ToConstant(new AuthProvider());*/
        }
    }
}