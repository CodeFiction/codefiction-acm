namespace CodeFiction.Acm.Contracts
{
    public interface IApplicationContextManager
    {
        void Initialize();
        void LoadStories();
        ApplicationContext ApplicationContext { get; set; }
    }
}