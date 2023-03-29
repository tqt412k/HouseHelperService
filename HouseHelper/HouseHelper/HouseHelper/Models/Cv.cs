using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseHelper.Models;

public partial class Cv
{
    public string Cvid { get; set; } = null!;

    public string? Worktype { get; set; }

    public DateTime? Workstartdate { get; set; }

    public DateTime? Workactualstart { get; set; }
    [NotMapped]
    public IFormFile ImgFile { get; set; }

    public virtual ICollection<Admin> Admins { get; } = new List<Admin>();

    public virtual ICollection<Jobseeker> Jobseekers { get; } = new List<Jobseeker>();
}
