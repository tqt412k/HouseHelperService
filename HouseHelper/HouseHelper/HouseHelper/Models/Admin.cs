using System;
using System.Collections.Generic;

namespace HouseHelper.Models;

public partial class Admin
{
    public string Adid { get; set; } = null!;

    public string? Adname { get; set; }

    public DateTime? Addob { get; set; }

    public int? Adphone { get; set; }

    public bool? Adgener { get; set; }

    public string? Adaddress { get; set; }

    public string? Ademail { get; set; }

    public string? Adimg { get; set; }

    public string? Jobid { get; set; }

    public string? JobSeekerid { get; set; }

    public string? FindHelperid { get; set; }

    public string? Cvid { get; set; }

    public virtual Cv? Cv { get; set; }

    public virtual Findhelper? FindHelper { get; set; }

    public virtual Job? Job { get; set; }

    public virtual Jobseeker? JobSeeker { get; set; }
}
