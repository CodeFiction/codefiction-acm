using CodeFiction.Acm.Contracts;

namespace DummyStoryManager.Activities
{
    public class ServiceActivity<T> : Activity<T>
    {
        public override object Execute(ActivityExecutingContext context)
        {
            context.ActivityParam = Param;
            return Action(Service, context);
        }
    }
}