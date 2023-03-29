using System;
using System.Collections.Generic;

namespace HouseHelper.Models;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string? UserName { get; set; }

    public string? Passwords { get; set; }

    public int? RoleId { get; set; }

    public string? Email { get; set; }

    public virtual Findhelper? Findhelper { get; set; }

    public virtual Jobseeker? Jobseeker { get; set; }

    public virtual Role? Role { get; set; }
}
