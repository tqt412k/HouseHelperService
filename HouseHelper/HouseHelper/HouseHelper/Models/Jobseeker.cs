using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseHelper.Models;

public partial class Jobseeker
{
    public string JobSeekerid { get; set; } = null!;

    public string? JobSeekername { get; set; }

    public DateTime? JobSeekerdob { get; set; }

    public string? JobSeekerphone { get; set; }

    public bool? JobSeekergender { get; set; }

    public string? JobSeekeraddress { get; set; }

    public string? JobSeekerimg { get; set; }

    public string? JobSeekerSalaryFrom { get; set; }

    public string? JobSeekerSalaryTo { get; set; }

    public string? JobSeekerDescription { get; set; }

    public string? JobSeekerEmail { get; set; }

    public string? JobSeekerLocation { get; set; }

    public string? JobSeekerCvid { get; set; }

    public virtual ICollection<Admin> Admins { get; } = new List<Admin>();

    public virtual User JobSeeker { get; set; } = null!;

    [NotMapped]
    public IFormFile ImgFile { get; set; }
    public virtual Cv? JobSeekerCv { get; set; }

    public virtual ICollection<Jobseekercookingskill> Jobseekercookingskills { get; } = new List<Jobseekercookingskill>();

    public virtual ICollection<Jobseekerlanguage> Jobseekerlanguages { get; } = new List<Jobseekerlanguage>();

    public virtual ICollection<Jobseekermainskill> Jobseekermainskills { get; } = new List<Jobseekermainskill>();

    public virtual ICollection<Jobseekerotherskill> Jobseekerotherskills { get; } = new List<Jobseekerotherskill>();
}
