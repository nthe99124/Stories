using System.ComponentModel.DataAnnotations;

namespace StoriesProject.API.Model.BaseEntity;

public partial class RoleAccountant
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid RoleId { get; set; }

    public Guid AccountId { get; set; }

    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

    public Guid? CreatedBy { get; set; }

    public virtual Accountant Account { get; set; } = null!;

    public virtual Accountant? CreatedByNavigation { get; set; }

    public virtual Role Role { get; set; } = null!;
}
