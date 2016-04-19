using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using SelfHostInRequestScopeMyTestedWebApi;
using SelfHostWebApiNinjectInRequestScope.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Dependencies;

[assembly: OwinStartup(typeof(Startup))]

namespace SelfHostInRequestScopeMyTestedWebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            app.UseNinjectMiddleware(ApiNinjectResolver.CreateKernel);
            app.UseNinjectWebApi(config);
        }
    }

    public class ApiNinjectResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public ApiNinjectResolver()
            : this(new StandardKernel())
        {
        }

        public static IKernel CreateKernel()
        {
            return new ApiNinjectResolver()._kernel;
        }

        public ApiNinjectResolver(IKernel ninjectKernel)
        {
            _kernel = ninjectKernel;
            _kernel.Bind<ICalc>().To<Calc>().InRequestScope();
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public T GetService<T>()
        {
            return _kernel.TryGet<T>();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        public void Dispose()
        {
        }
    }
}