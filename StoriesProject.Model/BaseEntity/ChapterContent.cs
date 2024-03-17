using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoriesProject.Model.BaseEntity;

/// <summary>
/// Bảng lưu thông tin các nội dung ảnh của chapter
/// </summary>
public partial class ChapterContent
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Description("Mã chapter")]
    public Guid ChapterId { get; set; }
    [Description("Vị trí")]
    public Guid SortOrder { get; set; }

    [Description("Tên chapter truyện")]
    public string? ImgLink { get; set; }

    [Description("Ngày tạo")]
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

    [Description("Người tạo")]
    public Guid? CreatedBy { get; set; }
    public virtual Accountant? CreatedByNavigation { get; set; }
    public virtual Chapter? ChapterIdNavigation { get; set; }
}
