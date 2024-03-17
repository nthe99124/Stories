using StoriesProject.Model.DTO;

namespace StoriesProject.Model.ViewModel.Story;

public class StoryByTopicParam
{
    public Guid TopicId { get; set; }
    public List<SortedPaging>? Sort { get; set; }
}
