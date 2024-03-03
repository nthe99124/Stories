using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoriesProject.Model.BaseEntity;

/// <summary>
/// Bảng lưu thông tin các chapter của truyện
/// </summary>
public partial class Chapter
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Description("Mã truyện")]
    public Guid StoryId { get; set; }

    [Description("Tên chapter truyện")]
    public string? Name { get; set; }

    [Description("Số chapter")]
    public int Version { get; set; }

    [Description("Nội dung chapter")]
    public string? Content { get; set; }

    [Description("Mô tả của chapter")]
    public string? Description { get; set; }

    [Description("Ngày tạo")]
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

    [Description("Người tạo")]
    public Guid? CreatedBy { get; set; }
    public virtual Accountant? CreatedByNavigation { get; set; }
    public virtual Story? StoryIdNavigation { get; set; }
}
