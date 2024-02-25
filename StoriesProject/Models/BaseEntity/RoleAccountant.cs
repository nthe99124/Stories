using System.ComponentModel.DataAnnotations;

namespace StoriesProject.Model.BaseEntity;

public partial class RoleAccountant
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid RoleId { get; set; }

    public Guid AccountId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public virtual Accountant Account { get; set; } = null!;

    public virtual Accountant? CreatedByNavigation { get; set; }

    public virtual Role Role { get; set; } = null!;
}
