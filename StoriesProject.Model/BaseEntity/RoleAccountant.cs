using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoriesProject.Model.BaseEntity;

/// <summary>
/// Bảng nhiều nhiều cho role và accoutant
/// </summary>
public partial class RoleAccountant
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Description("RoleId")]
    public Guid RoleId { get; set; }

    [Description("AccountId")]
    public Guid AccountId { get; set; }

    [Description("Ngày tạo")]
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

    [Description("Người tạo")]
    public Guid? CreatedBy { get; set; }

    public virtual Accountant Account { get; set; } = null!;

    public virtual Accountant? CreatedByNavigation { get; set; }

    public virtual Role Role { get; set; } = null!;
}
