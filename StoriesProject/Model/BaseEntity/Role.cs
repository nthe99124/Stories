using System;
using System.Collections.Generic;

namespace StoriesProject.Model.BaseEntity;

public partial class Role
{
    public Guid Id { get; set; }

    public string? RoleName { get; set; }

    public string? RoleDescription { get; set; }

    public virtual ICollection<RoleAccountant> RoleAccountants { get; set; } = new List<RoleAccountant>();
}
