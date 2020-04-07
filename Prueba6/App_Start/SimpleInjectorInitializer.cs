[assembly: WebActivator.PostApplicationStartMethod(typeof(Prueba6.App_Start.SimpleInjectorInitializer), "Initialize")]



namespace Prueba6.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;

    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;

    using pampasoft6;
    using pampasoft6.Data;
    using pampasoft6.Data.Repositorios;
    using pampasoft6.Models;
   

    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            // For instance:
            // container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
            container.Register<IRepositorio<localidades>, Repositorio<localidades>>(Lifestyle.Scoped);
        }
    }
}