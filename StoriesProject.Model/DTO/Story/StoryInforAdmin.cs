namespace StoriesProject.Model.BaseEntity;

public class StoryInforAdmin
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? AuthorName { get; set; }
    public long? Purchases { get; set; }
    public decimal? Revenue { get; set; }
    public long? ViewAccess { get; set; }
    public decimal? Price { get; set; }
    public string? Description { get; set; }
    public string? ImgLinkStory { get; set; }
    public string? TopicName { get; set; }
    public DateTime? CreatedDate { get; set; }
}
