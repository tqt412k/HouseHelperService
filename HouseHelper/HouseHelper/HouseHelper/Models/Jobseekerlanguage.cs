using System;
using System.Collections.Generic;

namespace HouseHelper.Models;

public partial class Jobseekerlanguage
{
    public string JobSeekerLanguageid { get; set; } = null!;

    public string? JobSeekerLanguagename { get; set; }

    public string? JobSeekerid { get; set; }

    public virtual Jobseeker? JobSeeker { get; set; }
}
