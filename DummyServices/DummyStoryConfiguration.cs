using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeFiction.Acm.Contracts;
using DummyStoryManager;
using DummyStoryManager.Activities;

namespace DummyServices
{
    public class DummyStoryConfiguration : IStoryConfiguration
    {
        public DummyStoryConfiguration()
        {
            // TODO : Temporery
                        Stories = new List<IStoryItem>
                {
                    (StoryItem) new StoryItem().AddRule("id", 12)
                                               .AddActivity<ServiceActivity<UserService>>(new UserService())
                                               .AddActivityBody((x, y) => x.GetUser(1))
                                               .AddResult(new StoryResultWrapper{ViewName = "~/Views/Account/ViewPathTest.cshtml"})
                                               .AddModel("id",0),
                                               
                    //ad param metoduna ihtiyaç var bir de add mapper'a parametre geçilecek. belki add activitybody için de bir generic parametre yazılıp modelbinder geçilebilir, paramını oradan alır...

                    (StoryItem) new StoryItem().AddRule("id", 12)
                                                .AddRule("test",13)
                                                .AddModel("id",0)
                                               .AddActivity<ServiceActivity<UserService>>(new UserService())
                                               //.AddActivityParam(new {id = ApplicationContextManager.Instance.GetParameter("aaa")})
                                                .AddActivityBody((x, y) => x.GetUser(Convert.ToInt32(y.RequestParameters["id"])))
                                               .AddActivity<ServiceActivity<UserService>>(new UserService())
                                               .AddActivityBody((x, y) => x.GetUser(y.LastResult.Id)),

                         (StoryItem) new StoryItem().AddRule(new{requestType = "GET", id = 15, test = 13})
                                               .AddActivity<ServiceActivity<UserService>>(new UserService())
                                               .AddActivityBody((x, y) => x.GetUser(1))
                                               .AddResult(new StoryResultWrapper{ ViewName = "StoryTest"}),

                        (StoryItem) new StoryItem().AddRule(new{requestType = "POST", id = 12})
                                               .AddActivity<ServiceActivity<UserService>>(new UserService())
                                               .AddActivityBody((x, y) => new RedirectToRouteResult("Default",null))
                                               .AddResult(new StoryResultWrapper{ViewName = "~/Views/Account/ViewPathTest.cshtml"})
                                               .AddModel("User",new User())

                };   
        }

        public List<IStoryItem> Stories { get; set; } 
    }
}
