using static StoriesProject.Model.Enum.DataType;

namespace StoriesProject.Model.BaseEntity;

public class ContentChapterGeneric
{
    public List<string> ImgLinkContent { get; set; }
    public string StoryName { get; set; }
    public string ChapterName { get; set; }
    public Guid StoryId { get; set; }
}
