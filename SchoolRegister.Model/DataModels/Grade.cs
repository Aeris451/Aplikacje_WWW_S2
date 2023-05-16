using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolRegister.Model.DataModels;

public class Grade
{
  [ForeignKey("GateOfIssue")]
  public DateTime DateOfIssue { get; set; }
  public GradeScale GradeValue { get; set; }
  public Subject Subject { get; set; } = null!;
  [ForeignKey("Subject")]
  public int SubjectId { get; set; }

  [ForeignKey("Student")]
  public int StudentId { get; set; }
  public Student Student { get; set; } = null!;

  public Grade()
  {

  }
}