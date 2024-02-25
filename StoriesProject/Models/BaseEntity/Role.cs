using System.ComponentModel.DataAnnotations;

namespace StoriesProject.Model.BaseEntity;

public partial class Role
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? RoleName { get; set; }

    public string? RoleDescription { get; set; }

    public virtual ICollection<RoleAccountant> RoleAccountants { get; set; } = new List<RoleAccountant>();
}
