using StoriesProject.Model.DTO;
using static StoriesProject.Model.Enum.DataType;

namespace StoriesProject.Model.BaseEntity;

public class StoryByTopicParam
{
    public Guid TopicId { get; set; }
    public List<SortedPaging>? Sort { get; set; }
}
