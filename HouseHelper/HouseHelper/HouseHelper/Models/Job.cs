using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseHelper.Models;

public partial class Job
{
    public string Jobid { get; set; } = null!;

    public string? JobName { get; set; }

    public string? JobType { get; set; }

    public DateTime? JobStart { get; set; }

    public string? JobStartFlexibility { get; set; }

    public DateTime? JobDatePost { get; set; }

    public string? JobGender { get; set; }

    public string? JobEducation { get; set; }

    public string? JobAge { get; set; }

    public string? JobExp { get; set; }

    public string? JobSalaryMin { get; set; }

    public string? JobSalryMax { get; set; }

    public string? JobImage { get; set; }

    public string? JobTitle { get; set; }

    public string? JobDescription { get; set; }

    public string? FindHelperid { get; set; }
    [NotMapped]
    public IFormFile ImgFile { get; set; }

    public virtual ICollection<Admin> Admins { get; } = new List<Admin>();

    public virtual Findhelper? FindHelper { get; set; }

    public virtual ICollection<Jobseekercookingskill> Jobseekercookingskills { get; } = new List<Jobseekercookingskill>();

    public virtual ICollection<Jobseekermainskill> Jobseekermainskills { get; } = new List<Jobseekermainskill>();

    public virtual ICollection<Jobseekerotherskill> Jobseekerotherskills { get; } = new List<Jobseekerotherskill>();
}
