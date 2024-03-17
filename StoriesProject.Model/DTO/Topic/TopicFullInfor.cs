namespace StoriesProject.Model.BaseEntity;

public class TopicFullInfor
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public long? NumberStory { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? Description { get; set; }
}
