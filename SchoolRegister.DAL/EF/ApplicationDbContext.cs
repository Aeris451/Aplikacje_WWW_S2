using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolRegister.Model.DataModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SchoolRegister.DAL.EF;
public class ApplicationDbContext : IdentityDbContext<User, Role, int>
{
  public virtual DbSet<Grade>? Grades { get; set; }
  public virtual DbSet<Group>? Groups { get; set; }
  public virtual DbSet<Subject>? Subjects { get; set; }
  public virtual DbSet<SubjectGroup>? SubjectGroups { get; set; }

  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    base.OnConfiguring(optionsBuilder);
    optionsBuilder.UseLazyLoadingProxies();
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<User>()
        .ToTable("AspNetUsers")
        .HasDiscriminator<int>("UseType")
        .HasValue<User>((int)RoleValue.User)
        .HasValue<Student>((int)RoleValue.Student)
        .HasValue<Parent>((int)RoleValue.Parent)
        .HasValue<Teacher>((int)RoleValue.Teacher);

    modelBuilder.Entity<SubjectGroup>()
        .HasKey(sg => new { sg.GroupId, sg.SubjectId });

    modelBuilder.Entity<SubjectGroup>()
        .HasOne(g => g.Group)
        .WithMany(sg => sg.SubjectGroups)
        .HasForeignKey(g => g.GroupId);

    modelBuilder.Entity<SubjectGroup>()
        .HasOne(s => s.Subject)
        .WithMany(sg => sg.SubjectGroups)
        .HasForeignKey(s => s.SubjectId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<Grade>().HasKey(g => new { g.DateOfIssue, g.SubjectId, g.StudentId });

    modelBuilder.Entity<Grade>()
        .HasOne(g => g.Student)
        .WithMany(s => s.Grades)
        .HasForeignKey(g => g.SubjectId)
        .OnDelete(DeleteBehavior.NoAction);

    modelBuilder.Entity<Grade>()
        .HasOne(g => g.Subject)
        .WithMany(s => s.Grades)
        .HasForeignKey(g => g.SubjectId)
        .OnDelete(DeleteBehavior.NoAction);

    modelBuilder.Entity<SubjectGroup>().HasKey(s => new { s.GroupId, s.SubjectId });

    modelBuilder.Entity<SubjectGroup>()
        .HasOne(g => g.Group)
        .WithMany(sg => sg.SubjectGroups)
        .HasForeignKey(g => g.GroupId)
        .OnDelete(DeleteBehavior.NoAction);

    modelBuilder.Entity<SubjectGroup>()
        .HasOne(s => s.Subject)
        .WithMany(sg => sg.SubjectGroups)
        .HasForeignKey(s => s.SubjectId)
        .OnDelete(DeleteBehavior.NoAction);
  }
}