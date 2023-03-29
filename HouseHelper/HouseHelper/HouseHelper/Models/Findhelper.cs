using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseHelper.Models;

public partial class Findhelper
{
    public string FindHelperid { get; set; } = null!;

    public string? FindHelperName { get; set; }

    public string? FindHelperEmail { get; set; }

    public string? FindHelperImg { get; set; }

    public string? FindHelperPhone { get; set; }

    public string? FindHelperAddress { get; set; }

    public string? FindHelperDescription { get; set; }

    public string? FindHelperLocation { get; set; }
    [NotMapped]
    public IFormFile ImgFile { get; set; }

    public virtual ICollection<Admin> Admins { get; } = new List<Admin>();

    public virtual User FindHelper { get; set; } = null!;

    public virtual ICollection<Job> Jobs { get; } = new List<Job>();
}
