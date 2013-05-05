using System;
using System.Collections.Generic;
using System.Web;
using CodeFiction.Acm.Contracts;

namespace CodeFiction.Acm.ApplicationContextManaging
{
    public class ApplicationContextManager : IApplicationContextManager
    {
        private readonly IStoryConfiguration _storyConfiguration;

        private IList<IStoryItem> _boundedStories;
        private IStoryItem _story;
        private IStoryItem _oldStep;
        public ApplicationContext ApplicationContext { get; set; }

        public ApplicationContextManager(IStoryConfiguration storyConfiguration)
        {
            _storyConfiguration = storyConfiguration;
        }

        public void Initialize()
        {
            // TODO : thread-saferty?
            _story = null;

            var httpContext = HttpContext.Current;

            ApplicationContext = new ApplicationContext();
            ApplicationContext.ContextParameters = new Dictionary<string, object>();

            ApplicationContext.ContextParameters.Add("requestType", httpContext.Request.RequestType);

            foreach (var item in httpContext.Request.Form.AllKeys)
            {
                ApplicationContext.ContextParameters.Add(item, httpContext.Request.Form[item]);
            }

            foreach (var item in httpContext.Request.QueryString.AllKeys)
            {
                ApplicationContext.ContextParameters.Add(item, httpContext.Request.QueryString[item]);
            }

            ApplicationContext.ContextParameters.Add("UserID", httpContext.Session["UserID"]);

            if (httpContext.Request.UrlReferrer != null)
            {
                ApplicationContext.ContextParameters.Add("UrlReferer", httpContext.Request.UrlReferrer.ToString());
            }

            ApplicationContext.ContextParameters.Add("Url", httpContext.Request.RawUrl);
        }

        public void LoadStories()
        {
            _boundedStories = _storyConfiguration.Stories;
            CheckStories();
        }

        public object GetParameter(string name)
        {
            if (ApplicationContext.ContextParameters.ContainsKey(name))
            {
                return ApplicationContext.ContextParameters[name];
            }
            return 0;
        }

        // TODO : thread-saferty?
        private void CheckStories()
        {
            HttpContext.Current.Items.Add("ForwardStory", false);
            foreach (var story in _boundedStories)
            {
                if (story.CheckStory(ApplicationContext))
                {
                    if (_story != null)
                    {
                        if (story.PassedRuleCount > _story.PassedRuleCount)
                        {
                            _story = story;
                        }
                    }
                    else
                    {
                        _story = story;
                        
                     
                    }
                    //break;
                }
            }

            ApplicationContext.Story = _story;
            HttpContext.Current.Items["ForwardStory"] = _story != null;
        }
    }
}
