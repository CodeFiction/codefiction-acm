using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeFiction.Acm.Contracts
{
    public interface IStoryItemConfigure
    {
        IStoryItemConfigure AddRule<T>(Expression<Func<IDictionary<string, object>, string, object, bool>> predicate);
        IStoryItemConfigure AddRule(string key, object value);
        IStoryItemConfigure AddRules(IDictionary<string, object> rules);
        IStoryItemConfigure AddRule(object rules);
        IStoryItemConfigure AddModel<T>(string name, T defaultValue);
        T AddActivity<T>() where T : class, IActivityExecute, new();
        T AddActivity<T>(object service) where T : class, IActivityExecute, new();
        IStoryItemConfigure AddMapper();
        IStoryItemConfigure AddResult(StoryResultWrapper resultWrapper);
    }
}