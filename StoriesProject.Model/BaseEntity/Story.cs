using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static StoriesProject.Model.Enum.DataType;

namespace StoriesProject.Model.BaseEntity;

/// <summary>
/// Bảng lưu thông tin truyện
/// </summary>
public partial class Story
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Description("Tên truyện")]
    public string? Name { get; set; }

    [Description("Mã truyện")]
    public string? Code { get; set; }

    [Description("Giá truyện")]
    public decimal Price { get; set; }

    [Description("Nội dung truyện - nếu là truyện chữ")]
    public string? Content { get; set; }

    [Description("Link ảnh tiêu đề")]
    public string? ImageLink { get; set; }

    [Description("Link video tiêu đề")]
    public string? VideoLink { get; set; }

    [Description("Mô tả ngắn của truyện")]
    public string? ShortDescription { get; set; }

    [Description("Mô tả của truyện")]
    public string? Description { get; set; }

    [Description("Lượt truy cập")]
    public long ViewAccess { get; set; } = 0;

    [Description("Lượt mua")]
    public long Purchases { get; set; } = 0;
    [Description("Trạng thái duyệt của truyện")]
    public StoryStatus Status { get; set; } = StoryStatus.NotApprovedYet;

    [Description("Điểm đánh giá")]
    public decimal RateScore { get; set; } = 0;

    [Description("Loại truyện")]
    public TypeOfStory TypeOfStory { get; set; }

    [Description("Đối tượng hướng tới")]
    public TypeOfStory TargetObject { get; set; }

    [Description("Nội dung thay đổi")]
    public string? ModifiedDescription { get; set; }

    [Description("Ngày tạo")]
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

    [Description("Người tạo")]
    public Guid? CreatedBy { get; set; }

    [Description("Ngày cập nhật")]
    public DateTime? ModifiedDate { get; set; }

    [Description("Người cập nhật")]
    public Guid? ModifiedBy { get; set; }

    public virtual Accountant? CreatedByNavigation { get; set; }

    public virtual Accountant? ModifiedByNavigation { get; set; }

    public virtual ICollection<TopicStory> TopicStories { get; set; } = new List<TopicStory>();
    public virtual ICollection<StoryAccoutant> StoryIDByNavigations { get; set; } = new List<StoryAccoutant>();
}
