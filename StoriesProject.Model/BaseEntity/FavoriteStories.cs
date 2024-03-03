using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static StoriesProject.Model.Enum.DataType;

namespace StoriesProject.Model.BaseEntity;

/// <summary>
/// Bảng lưu thông tin danh sách truyện yêu thích theo user
/// </summary>
public partial class FavoriteStories
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Description("Mã truyện")]
    public Guid StoryId { get; set; }

    [Description("Mã user")]
    public Guid AccountantId { get; set; }

    [Description("Ngày tạo")]
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

    public virtual Story? StoryIdNavigation { get; set; }
    public virtual Accountant? AccountantIdNavigation { get; set; }
}
