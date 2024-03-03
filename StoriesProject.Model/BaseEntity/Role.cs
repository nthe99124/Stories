using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoriesProject.Model.BaseEntity;

/// <summary>
/// Bảng lưu danh sách quyền
/// </summary>
public partial class Role
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Description("Tên của quyền")]
    public string? RoleName { get; set; }

    [Description("Mô tả quyền")]
    public string? RoleDescription { get; set; }

    public virtual ICollection<RoleAccountant> RoleAccountants { get; set; } = new List<RoleAccountant>();
}
