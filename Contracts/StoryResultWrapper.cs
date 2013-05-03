namespace CodeFiction.Acm.Contracts
{
    public class StoryResultWrapper
    {
        public object Mapper { get; set; }
        public object Model { get; set; }
        public string ViewName { get; set; }
        public string MasterName { get; set; }
        public int ViewType { get; set; }
        public string RouteName { get; set; }
        public string Url { get; set; }
        public object Route { get; set; }
    }
}