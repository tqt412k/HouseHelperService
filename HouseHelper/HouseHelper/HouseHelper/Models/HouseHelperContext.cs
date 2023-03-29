using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HouseHelper.Models;

public partial class HouseHelperContext : DbContext
{
    public HouseHelperContext()
    {
    }

    public HouseHelperContext(DbContextOptions<HouseHelperContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Cv> Cvs { get; set; }

    public virtual DbSet<Findhelper> Findhelpers { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Jobseeker> Jobseekers { get; set; }

    public virtual DbSet<Jobseekercookingskill> Jobseekercookingskills { get; set; }

    public virtual DbSet<Jobseekerlanguage> Jobseekerlanguages { get; set; }

    public virtual DbSet<Jobseekermainskill> Jobseekermainskills { get; set; }

    public virtual DbSet<Jobseekerotherskill> Jobseekerotherskills { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=TUANTQ;Database=HouseHelper;User Id=sa;Password=123;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Adid).HasName("PK__admin__7131D186B58F505E");

            entity.ToTable("admin");

            entity.Property(e => e.Adid).HasMaxLength(255);
            entity.Property(e => e.Adaddress).HasMaxLength(255);
            entity.Property(e => e.Addob).HasColumnType("date");
            entity.Property(e => e.Ademail).HasMaxLength(255);
            entity.Property(e => e.Adimg).HasMaxLength(255);
            entity.Property(e => e.Adname).HasMaxLength(255);
            entity.Property(e => e.Cvid)
                .HasMaxLength(50)
                .HasColumnName("CVid");
            entity.Property(e => e.FindHelperid).HasMaxLength(255);
            entity.Property(e => e.JobSeekerid).HasMaxLength(255);
            entity.Property(e => e.Jobid).HasMaxLength(255);

            entity.HasOne(d => d.Cv).WithMany(p => p.Admins)
                .HasForeignKey(d => d.Cvid)
                .HasConstraintName("FK__admin__CVid__66603565");

            entity.HasOne(d => d.FindHelper).WithMany(p => p.Admins)
                .HasForeignKey(d => d.FindHelperid)
                .HasConstraintName("FK__admin__FindHelpe__6754599E");

            entity.HasOne(d => d.JobSeeker).WithMany(p => p.Admins)
                .HasForeignKey(d => d.JobSeekerid)
                .HasConstraintName("FK__admin__JobSeeker__693CA210");

            entity.HasOne(d => d.Job).WithMany(p => p.Admins)
                .HasForeignKey(d => d.Jobid)
                .HasConstraintName("FK__admin__Jobid__68487DD7");
        });

        modelBuilder.Entity<Cv>(entity =>
        {
            entity.HasKey(e => e.Cvid).HasName("PK__CV__A0A8416BD6C0CC58");

            entity.ToTable("CV");

            entity.Property(e => e.Cvid)
                .HasMaxLength(50)
                .HasColumnName("CVid");
            entity.Property(e => e.Workactualstart).HasColumnType("date");
            entity.Property(e => e.Workstartdate).HasColumnType("date");
            entity.Property(e => e.Worktype).HasMaxLength(50);
        });

        modelBuilder.Entity<Findhelper>(entity =>
        {
            entity.HasKey(e => e.FindHelperid).HasName("PK__findhelp__22F9BEA2F25DD02D");

            entity.ToTable("findhelper");

            entity.Property(e => e.FindHelperid).HasMaxLength(255);
            entity.Property(e => e.FindHelperAddress).HasMaxLength(255);
            entity.Property(e => e.FindHelperDescription).HasMaxLength(255);
            entity.Property(e => e.FindHelperEmail).HasMaxLength(255);
            entity.Property(e => e.FindHelperImg).HasMaxLength(255);
            entity.Property(e => e.FindHelperLocation).HasMaxLength(50);
            entity.Property(e => e.FindHelperName).HasMaxLength(255);
            entity.Property(e => e.FindHelperPhone).HasMaxLength(255);

            entity.HasOne(d => d.FindHelper).WithOne(p => p.Findhelper)
                .HasForeignKey<Findhelper>(d => d.FindHelperid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_findhelper_Users");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Jobid).HasName("PK__job__05678CBA5B959B2D");

            entity.ToTable("job");

            entity.Property(e => e.Jobid).HasMaxLength(255);
            entity.Property(e => e.FindHelperid).HasMaxLength(255);
            entity.Property(e => e.JobAge).HasMaxLength(255);
            entity.Property(e => e.JobDatePost).HasColumnType("date");
            entity.Property(e => e.JobDescription).HasMaxLength(1000);
            entity.Property(e => e.JobEducation).HasMaxLength(255);
            entity.Property(e => e.JobExp).HasMaxLength(255);
            entity.Property(e => e.JobGender).HasMaxLength(255);
            entity.Property(e => e.JobImage).HasMaxLength(255);
            entity.Property(e => e.JobName).HasMaxLength(255);
            entity.Property(e => e.JobSalaryMin).HasMaxLength(255);
            entity.Property(e => e.JobSalryMax).HasMaxLength(255);
            entity.Property(e => e.JobStart).HasColumnType("date");
            entity.Property(e => e.JobStartFlexibility).HasMaxLength(255);
            entity.Property(e => e.JobTitle).HasMaxLength(255);
            entity.Property(e => e.JobType).HasMaxLength(255);

            entity.HasOne(d => d.FindHelper).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.FindHelperid)
                .HasConstraintName("FK_FindHelperid");
        });

        modelBuilder.Entity<Jobseeker>(entity =>
        {
            entity.HasKey(e => e.JobSeekerid).HasName("PK__jobseeke__8916268499082EE6");

            entity.ToTable("jobseeker");

            entity.Property(e => e.JobSeekerid).HasMaxLength(255);
            entity.Property(e => e.JobSeekerCvid)
                .HasMaxLength(50)
                .HasColumnName("JobSeekerCVid");
            entity.Property(e => e.JobSeekerDescription).HasMaxLength(1000);
            entity.Property(e => e.JobSeekerEmail).HasMaxLength(50);
            entity.Property(e => e.JobSeekerLocation).HasMaxLength(50);
            entity.Property(e => e.JobSeekerSalaryFrom).HasMaxLength(255);
            entity.Property(e => e.JobSeekerSalaryTo).HasMaxLength(255);
            entity.Property(e => e.JobSeekeraddress).HasMaxLength(255);
            entity.Property(e => e.JobSeekerdob).HasColumnType("date");
            entity.Property(e => e.JobSeekerimg).HasMaxLength(255);
            entity.Property(e => e.JobSeekername).HasMaxLength(255);
            entity.Property(e => e.JobSeekerphone).HasMaxLength(255);

            entity.HasOne(d => d.JobSeekerCv).WithMany(p => p.Jobseekers)
                .HasForeignKey(d => d.JobSeekerCvid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__jobseeker__JobSe__6C190EBB");

            entity.HasOne(d => d.JobSeeker).WithOne(p => p.Jobseeker)
                .HasForeignKey<Jobseeker>(d => d.JobSeekerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_jobseeker_Users");
        });

        modelBuilder.Entity<Jobseekercookingskill>(entity =>
        {
            entity.HasKey(e => e.JobSeekerCookingSkillid).HasName("PK__jobseeke__2D2EEB63666EDC06");

            entity.ToTable("jobseekercookingskill");

            entity.Property(e => e.JobSeekerCookingSkillid).HasMaxLength(255);
            entity.Property(e => e.JobSeekerCookingSkillname).HasMaxLength(255);
            entity.Property(e => e.JobSeekerid).HasMaxLength(255);
            entity.Property(e => e.Jobid).HasMaxLength(255);

            entity.HasOne(d => d.JobSeeker).WithMany(p => p.Jobseekercookingskills)
                .HasForeignKey(d => d.JobSeekerid)
                .HasConstraintName("FK_JobSeekerCookingSkill");

            entity.HasOne(d => d.Job).WithMany(p => p.Jobseekercookingskills)
                .HasForeignKey(d => d.Jobid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__jobseeker__Jobid__6E01572D");
        });

        modelBuilder.Entity<Jobseekerlanguage>(entity =>
        {
            entity.HasKey(e => e.JobSeekerLanguageid).HasName("PK__jobseeke__3B08EB7E021C1ABF");

            entity.ToTable("jobseekerlanguage");

            entity.Property(e => e.JobSeekerLanguageid).HasMaxLength(255);
            entity.Property(e => e.JobSeekerLanguagename).HasMaxLength(255);
            entity.Property(e => e.JobSeekerid).HasMaxLength(255);

            entity.HasOne(d => d.JobSeeker).WithMany(p => p.Jobseekerlanguages)
                .HasForeignKey(d => d.JobSeekerid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_JobSeekerid");
        });

        modelBuilder.Entity<Jobseekermainskill>(entity =>
        {
            entity.HasKey(e => e.JobSeekerMainSkillid).HasName("PK__jobseeke__D64A3BB58E26C77D");

            entity.ToTable("jobseekermainskill");

            entity.Property(e => e.JobSeekerMainSkillid).HasMaxLength(255);
            entity.Property(e => e.JobSeekerMainSkillname).HasMaxLength(255);
            entity.Property(e => e.JobSeekerid).HasMaxLength(255);
            entity.Property(e => e.Jobid).HasMaxLength(255);

            entity.HasOne(d => d.JobSeeker).WithMany(p => p.Jobseekermainskills)
                .HasForeignKey(d => d.JobSeekerid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_JobSeekerMainSkill");

            entity.HasOne(d => d.Job).WithMany(p => p.Jobseekermainskills)
                .HasForeignKey(d => d.Jobid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__jobseeker__Jobid__70DDC3D8");
        });

        modelBuilder.Entity<Jobseekerotherskill>(entity =>
        {
            entity.HasKey(e => e.JobSeekerOtherSkillid).HasName("PK__jobseeke__B3DA397EC1D9D74A");

            entity.ToTable("jobseekerotherskill");

            entity.Property(e => e.JobSeekerOtherSkillid).HasMaxLength(255);
            entity.Property(e => e.JobSeekerOtherSkillname).HasMaxLength(255);
            entity.Property(e => e.JobSeekerid).HasMaxLength(255);
            entity.Property(e => e.Jobid).HasMaxLength(255);

            entity.HasOne(d => d.JobSeeker).WithMany(p => p.Jobseekerotherskills)
                .HasForeignKey(d => d.JobSeekerid)
                .HasConstraintName("FK_JobSeekerOtherSkill");

            entity.HasOne(d => d.Job).WithMany(p => p.Jobseekerotherskills)
                .HasForeignKey(d => d.Jobid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__jobseeker__Jobid__72C60C4A");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__role__8AFACE1A07BF0514");

            entity.ToTable("role");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.RoleName).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__1788CCAC082DD7B8");

            entity.ToTable("users");

            entity.Property(e => e.UserId)
                .HasMaxLength(255)
                .HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Passwords).HasMaxLength(255);
            entity.Property(e => e.UserName).HasMaxLength(255);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_RoleId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
