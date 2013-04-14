using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyStoryManager
{
    public static class StoryManager
    {
        public static List<Story> Stories { get; set; }

        public static void InitStories()
        {
            Stories = new List<Story>();

            Stories.Add(new Story() { StoryId = 1, StoryName = "Story1" });
            Stories.Add(new Story() { StoryId = 2, StoryName = "Story2" });
        }

        public static StoryResult ExecuteStory(Story story)
        {
            return story.Next();
        }
    }

    public class StoryResult
    {
    }


    public class Story
    {
        public int StoryId { get; set; }
        public string StoryName { get; set; }

        public StoryResult Next()
        {
            return new StoryResult();
        }
    }
}
