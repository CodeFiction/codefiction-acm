using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using Castle.Windsor;
using CodeFiction.Acm.ApplicationContextManaging.Utils;
using CodeFiction.Acm.Contracts;

using IDependencyResolver = CodeFiction.Stack.Library.CoreContracts.IDependencyResolver;

namespace CodeFiction.Acm.ApplicationContextManaging.Web
{
    public class AcmControllerFactory : DefaultControllerFactory, IAcmControllerFactory
    {
        private readonly IDependencyResolver _dependencyResolver;
        private readonly IApplicationContextManager _applicationContextManager;

        public AcmControllerFactory(IDependencyResolver dependencyResolver, IApplicationContextManager applicationContextManager)
        {
            _dependencyResolver = dependencyResolver;
            _applicationContextManager = applicationContextManager;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            _applicationContextManager.Initialize();
            _applicationContextManager.LoadStories();


            ApplicationContext applicationContext = _applicationContextManager.ApplicationContext;

            if (applicationContext == null)
            {
                //TODO: throw exception
            }

            if (requestContext.HasStory())
            {
                var acmController = new AcmController(applicationContext)
                {
                    ActionInvoker = new AcmActionInvoker(applicationContext)
                };

                return acmController;
            }

            return _dependencyResolver.Resolve<IController>(controllerType.Name);
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

        public override void ReleaseController(IController controller)
        {
            try
            {
                _dependencyResolver.TearDown(controller);
            }
            catch (Exception)
            {
                // TODO: throw exception
                throw;
            }
        } 
    }
}