using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

using CodeFiction.Acm.ApplicationContextManaging.Utils;
using CodeFiction.Acm.Contracts;

namespace CodeFiction.Acm.ApplicationContextManaging.Web
{
    public class AcmControllerFactory : IAcmControllerFactory
    {
        public event AcmContextEventHandler OnControllerCreation;
        public event EventHandler OnControllerCreated;

        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            ApplicationContext applicationContext =
                OnOnControllerCreationHandler(new AcmEventArgs
                    {ControllerName = controllerName, RequestContext = requestContext});

            if (applicationContext == null)
            {
                //TODO: throw exception
            }

            AcmController controller = null;

            if (requestContext.HasStory())
            {
                controller = new AcmController(applicationContext)
                    {
                        ActionInvoker = new AcmActionInvoker(applicationContext)
                    };
            }

            OnControllerCreatedEventHandler(EventArgs.Empty);

            return controller;
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

        public void ReleaseController(IController controller)
        {
            var disposable = controller as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        private ApplicationContext OnOnControllerCreationHandler(AcmEventArgs e)
        {
            ApplicationContext applicationContext = null;

            AcmContextEventHandler handler = OnControllerCreation;
            if (handler != null)
            {
                applicationContext = handler(this, e);
            }

            return applicationContext;
        }

        private void OnControllerCreatedEventHandler(EventArgs e)
        {
            EventHandler handler = OnControllerCreated;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}