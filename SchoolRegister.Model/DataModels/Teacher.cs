using Microsoft.AspNetCore.Identity;
using System;

namespace SchoolRegister.Model.DataModels;

public class Teacher
{
  public IList<Subject> Subjects { get; set; } = null!;
  public string Title { get; set; }

  public Teacher()
  {
    Title = " ";
  }
}