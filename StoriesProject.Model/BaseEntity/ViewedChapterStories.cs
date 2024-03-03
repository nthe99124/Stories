using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoriesProject.Model.BaseEntity;

/// <summary>
/// Bảng lưu danh sách truyện đã lưu theo user => có bảng lưu truyện rồi nhưng để phục vụ lấy dữ liệu thì lưu thêm
/// </summary>
public partial class ViewedChapterStories
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Description("Mã chapter")]
    public Guid ChapterId { get; set; }

    [Description("Mã user")]
    public Guid AccountantId { get; set; }

    [Description("Cờ đánh dấu có phải là bản cao nhất không")]
    public Guid IsLastest { get; set; }

    [Description("Ngày đọc")]
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

    public virtual Chapter? ChapterIdNavigation { get; set; }
    public virtual Accountant? AccountantIdNavigation { get; set; }
}

