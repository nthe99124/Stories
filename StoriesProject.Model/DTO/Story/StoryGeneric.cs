using static StoriesProject.Model.Enum.DataType;

namespace StoriesProject.Model.BaseEntity;

public class StoryGeneric: StoryAccountGeneric
{
    public decimal Price { get; set; }
    public long ViewAccess { get; set; } = 0;
    public decimal RateScore { get; set; } = 0;
}
