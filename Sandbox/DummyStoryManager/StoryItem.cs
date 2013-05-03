using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CodeFiction.Acm.Contracts;

namespace DummyStoryManager
{
    public class StoryItem:IStoryItemConfigure, IStoryItem
    {
        public bool CancelResult { get; set; }
        public IDictionary<string, object> StoryContext { get; set; }
        //public IDictionary<string, MethodInfo> StoryExecutionContext { get; set; }
        public ICollection<IActivityExecute> Activities { get; set; }
        public ActivityExecutingContext ActivityContext { get; set; }
        public IDictionary<string,object> Models { get; set; }
        public StoryResultWrapper ResultWrapper { get; set; }
        public int PassedRuleCount { get; set; }

        public StoryItem()
        {
            StoryContext = new Dictionary<string, object>();
            ActivityContext = new ActivityExecutingContext();
            Activities = new List<IActivityExecute>();
            ResultWrapper = new StoryResultWrapper();
            Models = new Dictionary<string, object>();
        }

        public object Run(IDictionary<string, object> parameters)
        {
            object result = null;
            ActivityContext.RequestParameters = parameters;

            if (this.OnAuthorization())
            {
                this.OnStoryEcxecution();
                foreach (var activity in Activities)
                {
                    result = activity.Execute(ActivityContext);
                    ActivityContext.LastResult = result;
                }
                this.OnStoryExecuted();
            }
            return result;

        }

        protected virtual void OnStoryEcxecution()
        {


        }

        protected virtual void OnStoryExecuted()
        {
        }

        protected virtual bool OnAuthorization()
        {
            return true;
        }

        public IStoryItemConfigure AddRule(Expression<Func<IDictionary<string,object>, string, object, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IStoryItemConfigure AddModel<T>(string name, T defaultValue)
        {
            if (Models.ContainsKey(name))
            {
                Models[name] = defaultValue;
            }
            else
            {
                Models.Add(name,defaultValue);
            }

            return this;
        }

        public T AddActivity<T>() where T : class, IActivityExecute, new()
        {
            if (this.Activities == null)
            {
                this.Activities = new Collection<IActivityExecute>();
            }
            var activity = new T();
            this.Activities.Add(activity);
            activity.StoryItem = this;
            return activity;
        }

        public T AddActivity<T>(object service) where T : class, IActivityExecute, new()
        {
            if (this.Activities == null)
            {
                this.Activities = new Collection<IActivityExecute>();
            }
            var activity = new T();
            activity.SetService(service);
            this.Activities.Add(activity);
            activity.StoryItem = this;
            return activity;
        }

        public IStoryItemConfigure AddMapper()
        {
            throw new NotImplementedException();
        }

        public IStoryItemConfigure AddRule<T>(Expression<Func<IDictionary<string, object>, string, object, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IStoryItemConfigure AddRule(string key, object value)
        {
            this.StoryContext.Add(key,value);
            return this;
        }

        public IStoryItemConfigure AddRule(object rules)
        {
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(rules))
            {
                object obj2 = descriptor.GetValue(rules);
                
                this.StoryContext.Add(descriptor.Name, obj2);
            }

            return this;
        }

        public IStoryItemConfigure AddRules(IDictionary<string, object> rules)
        {
            this.StoryContext = rules;
            return this;
        }


        public bool CheckStory(ApplicationContext context)
        {
            List<bool> checker = new List<bool>();
            if (StoryContext != null)
            {
                foreach (var sc in StoryContext)
                {
                    if (context.ContextParameters.ContainsKey(sc.Key))
                    {
                        checker.Add(context.ContextParameters[sc.Key].ToString() == sc.Value.ToString());
                    }
                    else
                    {
                        checker.Add(false);
                    }
                }
            }
            PassedRuleCount = checker.Count(c => c);
            return checker.Count > 0 && checker.All(c => c);
        }




        public IList<ParameterInfo> GetParameters()
        {
            return this.Models.Select(model => new ReflectedParameterInfo(model.Key, model.Value, model.Value.GetType())).Cast<ParameterInfo>().ToList();
        }


        public IStoryItemConfigure AddResult(StoryResultWrapper resultWrapper)
        {
            ResultWrapper = resultWrapper;
            return this;
        }
    }
}
