using System;
using System.Collections.Generic;

namespace StoriesProject.Model.BaseEntity;

public partial class RoleAccountant
{
    public Guid Id { get; set; }

    public Guid RoleId { get; set; }

    public Guid AccountId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public virtual Accountant Account { get; set; } = null!;

    public virtual Accountant? CreatedByNavigation { get; set; }

    public virtual Role Role { get; set; } = null!;
}
