using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static StoriesProject.Model.Enum.DataType;

namespace StoriesProject.Model.BaseEntity;

/// <summary>
/// Bảng lưu thông tin tài khoản
/// </summary>
public partial class Accountant
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(50, ErrorMessage = "UserName quá dài")]
    [Required(ErrorMessage = "UserName chưa có giá trị")]
    [Description("Tên đăng nhập")]
    public string UserName { get; set; }

    [StringLength(50, ErrorMessage = "Password quá dài")]
    [Required(ErrorMessage = "Password chưa có giá trị")]
    [Description("Password")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Ngôn ngữ chưa có giá trị")]
    [Description("Ngôn ngữ")]
    public string Language { get; set; } = "vi-VN";

    [Description("Cờ đánh dấu tài khoản có bị khóa không")]
    public bool IsLocked { get; set; }

    [Description("Họ và tên")]
    public string? Name { get; set; }

    [Description("Số điện thoại")]
    [StringLength(20, ErrorMessage = "Số điện thoại quá dài")]
    public string? PhoneNumber { get; set; }

    [Description("Email")]
    public string? Email { get; set; }

    [Description("Giới tính")]
    public GenderType Gender { get; set; } = GenderType.Other;

    [Description("Ngày sinh")]
    public DateTime? DateOfBirth { get; set; } = DateTime.UtcNow;

    [Description("Giới thiệu bản thân")]
    public string? Introduce { get; set; }

    [Description("Link avatar")]
    public string? ImgAvatar { get; set; }

    [Description("Số coin tài khoảng đang có")]
    public long Coin { get; set; }

    [Description("Số truyện đã tạo (với tài khoản là tác giả)")]
    public long? StoryCreated { get; set; }

    [Description("Ngày tạo")]
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

    public virtual CoinLog IdNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<RoleAccountant> RoleAccountantAccounts { get; set; } = new List<RoleAccountant>();

    public virtual ICollection<RoleAccountant> RoleAccountantCreatedByNavigations { get; set; } = new List<RoleAccountant>();

    public virtual ICollection<Story> StoryCreatedByNavigations { get; set; } = new List<Story>();

    public virtual ICollection<Story> StoryModifiedByNavigations { get; set; } = new List<Story>();
    public virtual ICollection<StoryAccoutant> AccoutantIDByNavigations { get; set; } = new List<StoryAccoutant>();

    public virtual AuthorRegister AuthorRegister { get; set; } = null;
}
