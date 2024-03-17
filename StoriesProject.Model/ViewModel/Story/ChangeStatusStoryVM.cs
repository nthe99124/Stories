using static StoriesProject.Model.Enum.DataType;

namespace StoriesProject.Model.ViewModel.Story;

public class ChangeStatusStoryVM
{
    public Guid StoryId { get; set; }
    public StoryStatus Status { get; set; }
}
