using static StoriesProject.Model.Enum.DataType;

namespace StoriesProject.Model.BaseEntity;

public class StoryAccountGeneric
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ImageLink { get; set; }
    public string? VideoLink { get; set; }
    public string? Description { get; set; }
    public TypeOfStory TypeOfStory { get; set; }
}
