using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoriesProject.Model.BaseEntity;

/// <summary>
/// Bảng lưu thông tin nhiều nhiều giữa người đọc và truyện
/// </summary>
public partial class StoryAccoutant
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Description("Người đọc")]
    public Guid AccoutantID { get; set; }

    [Description("Mã truyện")]
    public string? StoryID { get; set; }

    [Description("Cờ đánh dấu đã đọc xong chưa")]
    public bool IsReaded { get; set; }

    [Description("Ngày bắt đầu đọc")]
    public DateTime? StartDate { get; set; } = DateTime.UtcNow;

    [Description("Ngày kết thúc đọc")]
    public DateTime? EndDate { get; set; } = DateTime.UtcNow;

    public virtual Story? StoryIDNavigation { get; set; }

    public virtual Accountant? AccoutantIDNavigation { get; set; }
}
