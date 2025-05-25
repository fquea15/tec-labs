using System;
using System.Collections.Generic;
using Lab10.Domain.Enum;

namespace Lab10.Domain.Entities;

public partial class Role
{
    public Guid Id { get; set; }
    
    public RoleName RoleName { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
