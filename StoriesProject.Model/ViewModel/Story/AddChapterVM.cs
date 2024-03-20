namespace StoriesProject.Model.BaseEntity;

/// <summary>
/// Bảng lưu thông tin các chapter của truyện
/// </summary>
public partial class AddChapterVM
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid StoryId { get; set; }
    public string? Name { get; set; }
    public int Version { get; set; } = 0;
    public string? Description { get; set; }
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
    public Guid? CreatedBy { get; set; }
}
