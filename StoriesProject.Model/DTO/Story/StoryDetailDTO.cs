using static StoriesProject.Model.Enum.DataType;

namespace StoriesProject.Model.DTO.Story
{
    public class StoryDetailFullDTO
    {
        public StoryDetailDTO? DetailStory { get; set; }
        public List<ChapterslDTO> Chapter { get; set; }
        public List<TopicslDTO> Topic { get; set; }
    }
    public class StoryDetailDTO
    {
        public string StoryName { get; set; }
        public string AuthorName { get; set; }
        public int TotalStory { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string ImageLink { get; set; }
        public decimal Price { get; set; }
        public double RateScore { get; set; }
        public TypeOfStory TypeOfStory { get; set; }
        public string VideoLink { get; set; }
        public int ViewAccess { get; set; }
    }

    public class ChapterslDTO
    {
        public string Content { get; set; }
        public string Name { get; set; }
        public Guid Id { get; set; }
    }

    public class TopicslDTO
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
    }
}
