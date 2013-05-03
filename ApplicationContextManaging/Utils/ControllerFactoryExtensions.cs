using System.Web.Routing;

namespace CodeFiction.Acm.ApplicationContextManaging.Utils
{
    public static class ControllerFactoryExtensions
    {
        public static bool HasStory(this RequestContext context)
        {
            bool forward = false;
            bool.TryParse(context.HttpContext.Items["ForwardStory"].ToString(), out forward);
            return forward;
        }
    }
}