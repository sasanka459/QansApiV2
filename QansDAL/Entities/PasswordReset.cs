using System;
using System.Collections.Generic;

namespace QansDAL.Entities;

public partial class PasswordReset
{
    public int ResetId { get; set; }

    public int? UserId { get; set; }

    public string ResetToken { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public virtual User? User { get; set; }
}
