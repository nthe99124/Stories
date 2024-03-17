using static StoriesProject.Model.Enum.DataType;

namespace StoriesProject.Model.ViewModel.Story;

public class StoryRegisterVM
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? ImageLink { get; set; }
    public string? Description { get; set; }
    public string? ShortDescription { get; set; }
    public List<Guid>? ListTopic { get; set; }
    public string? VideoLink { get; set; }
    public TargetObject TargetObject { get; set; }
}
