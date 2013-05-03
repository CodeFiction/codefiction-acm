using System;

namespace CodeFiction.Acm.Contracts
{
    public interface IActivityConfigure<T>
    {
        IActivityConfigure<T> AddActivityParam(object param);
        IStoryItemConfigure AddActivityBody(Func<T, ActivityExecutingContext, object> action);

    }
}