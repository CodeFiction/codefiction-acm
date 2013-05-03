using System.Collections.Generic;

namespace CodeFiction.Acm.Contracts
{
    public class ActivityExecutingContext
    {
        public dynamic ActivityParam { get; set; }

        public dynamic LastResult { get; set; }

        public IDictionary<string, object> RequestParameters { get; set; }
    }
}