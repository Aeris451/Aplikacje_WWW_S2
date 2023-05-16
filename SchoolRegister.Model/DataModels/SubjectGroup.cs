using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolRegister.Model.DataModels;

public class SubjectGroup
{
  public Subject Subject { get; set; } = null!;

  [ForeignKey("Subject")]
  public int SubjectId { get; set; }
  public Group? Group { get; set; }

  [ForeignKey("Group")]
  public int GroupId { get; set; }

  public SubjectGroup()
  {

  }
}