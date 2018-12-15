using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace beltexam.Models
{
  public class UserAndActivity
  {
    [Key]
    public int UserAndActivityId { get; set; }
    public int UserId { get; set; }
    public int ActivityId { get; set; }
    public User User { get; set; }
    public Activity Activity { get; set; }
    public UserAndActivity() { }


  }
}