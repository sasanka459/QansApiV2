using System;
using System.Collections.Generic;

namespace QansDAL.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<PasswordReset> PasswordResets { get; set; } = new List<PasswordReset>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
