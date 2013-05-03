using System.Collections.Generic;

namespace CodeFiction.Acm.Contracts
{
    public interface IStoryConfiguration
    {
        List<IStoryItem> Stories { get; set; }
    }
}