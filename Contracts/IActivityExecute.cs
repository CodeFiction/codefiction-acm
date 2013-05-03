namespace CodeFiction.Acm.Contracts
{
    public interface IActivityExecute
    {
        object Execute(ActivityExecutingContext context);
        IStoryItem StoryItem { get; set; }
        void SetService(object service);
    }
}