using System.ComponentModel.DataAnnotations;

namespace StoriesProject.Model.BaseEntity;

public partial class TopicStory
{
    [Key]
    public Guid Id { get; set; }

    public Guid TopicId { get; set; }

    public Guid StoryId { get; set; }

    public virtual Story Story { get; set; } = null!;

    public virtual Topic Topic { get; set; } = null!;
}
