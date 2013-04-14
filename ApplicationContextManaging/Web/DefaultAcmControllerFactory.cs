using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using CodeFiction.Acm.ApplicationContextManaging.Contracts;

namespace CodeFiction.Acm.ApplicationContextManaging.Web
{
    public class DefaultAcmControllerFactory : IAcmControllerFactory
    {
        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            throw new NotImplementedException();
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            throw new NotImplementedException();
        }

        public void ReleaseController(IController controller)
        {
            throw new NotImplementedException();
        }
    }
}
