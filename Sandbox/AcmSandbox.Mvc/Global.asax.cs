using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

using AcmSandbox.Mvc.App_Start;

using CodeFiction.Acm.ApplicationContextManaging;
using CodeFiction.Acm.ApplicationContextManaging.Web;
using CodeFiction.Acm.Contracts;
using CodeFiction.Stack.Library.Core.Initializers;
using CodeFiction.Stack.Library.CoreContracts;

using DummyServices;

using IDependencyResolver = CodeFiction.Stack.Library.CoreContracts.IDependencyResolver;

namespace AcmSandbox.Mvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly Bootstrapper Bootstrapper;

        static MvcApplication()
        {
            Bootstrapper = Bootstrapper.Create();

            Bootstrapper.RegisterComponent(resolver => resolver.Register<IApplicationContextManager, ApplicationContextManager>(InstanceMode.Singleton));
            Bootstrapper.RegisterComponent(resolver => resolver.Register<IAcmControllerFactory, AcmControllerFactory>());
            Bootstrapper.RegisterComponent(resolver => resolver.Register<IStoryConfiguration, DummyStoryConfiguration>());
            Bootstrapper.RegisterComponent(resolver => resolver.RegisterInstance(typeof(ControllerBuilder), ControllerBuilder.Current));
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            IDependencyResolver dependencyResolver = Bootstrapper.DependencyResolver;

            ControllerBuilder.Current.SetControllerFactory(new AcmControllerFactory(dependencyResolver, dependencyResolver.Resolve<IApplicationContextManager>()));

            RegisterControllers();
        }

        private void RegisterControllers()
        {
            var type = typeof (IController);
            var types = AppDomain.CurrentDomain.GetAssemblies().ToList()
                .SelectMany(s => s.GetTypes())
                .Where(type.IsAssignableFrom).ToList();

            IDependencyResolver dependencyResolver = Bootstrapper.DependencyResolver;

            foreach (var controllerType in types)
            {
                Type controller = controllerType;

                dependencyResolver.Register(typeof (IController), controller, InstanceMode.Transient, controller.Name);
            }
        }

        protected void Application_BeginRequest()
        {

        }

        protected void Application_AuthenticateRequest()
        {

        }

        protected void Application_EndRequest()
        {

        }

        protected void Application_End()
        {

        }

        protected void Application_Error()
        {
            Exception lastError = Context.Server.GetLastError();
        }

        protected void Session_Start()
        {

        }

        protected void Session_End()
        {

        }

//        Application_Init: Fired when an application initializes or is first called. It's invoked for all HttpApplication object instances.
//Application_Disposed: Fired just before an application is destroyed. This is the ideal location for cleaning up previously used resources.
//Application_Error: Fired when an unhandled exception is encountered within the application.
//Application_Start: Fired when the first instance of the HttpApplication class is created. It allows you to create objects that are accessible by all HttpApplication instances.
//Application_End: Fired when the last instance of an HttpApplication class is destroyed. It's fired only once during an application's lifetime.
//Application_BeginRequest: Fired when an application request is received. It's the first event fired for a request, which is often a page request (URL) that a user enters.
//Application_EndRequest: The last event fired for an application request.
//Application_PreRequestHandlerExecute: Fired before the ASP.NET page framework begins executing an event handler like a page or Web service.
//Application_PostRequestHandlerExecute: Fired when the ASP.NET page framework is finished executing an event handler.
//Applcation_PreSendRequestHeaders: Fired before the ASP.NET page framework sends HTTP headers to a requesting client (browser).
//Application_PreSendContent: Fired before the ASP.NET page framework sends content to a requesting client (browser).
//Application_AcquireRequestState: Fired when the ASP.NET page framework gets the current state (Session state) related to the current request.
//Application_ReleaseRequestState: Fired when the ASP.NET page framework completes execution of all event handlers. This results in all state modules to save their current state data.
//Application_ResolveRequestCache: Fired when the ASP.NET page framework completes an authorization request. It allows caching modules to serve the request from the cache, thus bypassing handler execution.
//Application_UpdateRequestCache: Fired when the ASP.NET page framework completes handler execution to allow caching modules to store responses to be used to handle subsequent requests.
//Application_AuthenticateRequest: Fired when the security module has established the current user's identity as valid. At this point, the user's credentials have been validated.
//Application_AuthorizeRequest: Fired when the security module has verified that a user can access resources.
//Session_Start: Fired when a new user visits the application Web site.
//Session_End: Fired when a user's session times out, ends, or they leave the application Web site.
    }
}   