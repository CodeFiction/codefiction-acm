using System.Collections.Generic;
using System.Reflection;

namespace CodeFiction.Acm.Contracts
{
    public interface IStoryItem
    {
        object Run(IDictionary<string, object> parameters);
        bool CheckStory(ApplicationContext context);
        int PassedRuleCount { get; set; }
        IList<ParameterInfo> GetParameters();
        StoryResultWrapper ResultWrapper { get; set; }
    }
}
