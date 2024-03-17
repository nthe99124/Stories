using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoriesProject.Model.BaseEntity;

/// <summary>
/// Bảng lưu các chủ đề của truyện
/// </summary>
public partial class Topic
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Description("Tên chủ đề")]
    public string? Name { get; set; }

    [Description("Mô tả")]
    public string? Description { get; set; }

    [Description("Ngày tạo")]
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

    [Description("Người tạo")]
    public Guid? CreatedBy { get; set; }

    public virtual ICollection<TopicStory> TopicStories { get; set; } = new List<TopicStory>();
}
