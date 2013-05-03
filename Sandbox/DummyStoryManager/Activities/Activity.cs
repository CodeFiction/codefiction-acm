using System;
using CodeFiction.Acm.Contracts;

namespace DummyStoryManager.Activities
{
    public abstract class Activity<T> : IActivityConfigure<T>, IActivityExecute
    {
        public IStoryItem StoryItem { get; set; }

        public T Service { get; set; }
        public Func<T, ActivityExecutingContext, object> Action { get; set; }
        public object Param { get; set; }
        public object Result { get; set; }

        public virtual IActivityConfigure<T> AddActivityParam(object param)
        {
            Param = param;
            return this;
        }

        public virtual IStoryItemConfigure AddActivityBody(Func<T, ActivityExecutingContext, object> action)
        {
            Action = action;
            return StoryItem as IStoryItemConfigure;
        }

        public abstract object Execute(ActivityExecutingContext context);


        public void SetService(object service)
        {
            Service = (T)service;
        }
    }
}