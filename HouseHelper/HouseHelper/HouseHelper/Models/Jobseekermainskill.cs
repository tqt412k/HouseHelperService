using System;
using System.Collections.Generic;

namespace HouseHelper.Models;

public partial class Jobseekermainskill
{
    public string JobSeekerMainSkillid { get; set; } = null!;

    public string? JobSeekerMainSkillname { get; set; }

    public string? JobSeekerid { get; set; }

    public string? Jobid { get; set; }

    public virtual Job? Job { get; set; }

    public virtual Jobseeker? JobSeeker { get; set; }
}
