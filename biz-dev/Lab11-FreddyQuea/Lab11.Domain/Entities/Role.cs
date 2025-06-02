using Lab11.Domain.Enums;

namespace Lab11.Domain.Entities;

public partial class Role
{
    public Guid RoleId { get; set; }

    public RoleType RoleName { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}