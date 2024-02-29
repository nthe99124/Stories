using System.ComponentModel.DataAnnotations;

namespace StoriesProject.API.Model.BaseEntity;

public partial class Topic
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

    public Guid? CreatedBy { get; set; }

    public virtual ICollection<TopicStory> TopicStories { get; set; } = new List<TopicStory>();
}
