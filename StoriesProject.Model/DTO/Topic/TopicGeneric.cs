using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoriesProject.Model.BaseEntity;

public class TopicGeneric
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
}
