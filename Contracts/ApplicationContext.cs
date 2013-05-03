using System.Collections.Generic;

namespace CodeFiction.Acm.Contracts
{
    public class ApplicationContext
    {
        public IDictionary<string, object> ContextParameters { get; set; }
        public IStoryItem Story { get; set; }
    }
}