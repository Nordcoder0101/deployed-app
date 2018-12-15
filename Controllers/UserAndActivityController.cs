using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using beltexam.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace beltexam.Controllers
{
  public class UserAndActivityController : Controller
  {
    private beltexamContext dbContext;
    
    public UserAndActivityController(beltexamContext context)
    {
      dbContext = context;
      
    }
    [HttpGet("Home")]
    public IActionResult ShowHome()
    {
      if (HttpContext.Session.GetInt32("UserId") == null)
      {
        return View("Index");
      }
      int? LoggedInUserId = HttpContext.Session.GetInt32("UserId");
      var LoggedInUser = dbContext.Users.FirstOrDefault(u => u.UserId == LoggedInUserId);
      ViewBag.LoggedInUser = LoggedInUser;
      var AllActivities = dbContext.Activities
      .Include(uaa => uaa.UserAndActivities)
      .ThenInclude(u => u.User).OrderBy(u => u.Date).ToList();



      ViewBag.AllActivities = AllActivities;
      return View();
    }

    [HttpPost("CreateUserAndActivity")]
    public IActionResult CreateUserAndActivity(UserAndActivity NewUserAndActivity)
    {
      dbContext.Add(NewUserAndActivity);
      dbContext.SaveChanges();
      return RedirectToAction("ShowHome");
    }

    [HttpGet("delete/{id}")]
    public IActionResult DeleteUserAndActivity(int Id)
    {
      var RecordToDelete = dbContext.UsersAndActivities.FirstOrDefault(i => i.ActivityId == Id);
      dbContext.Remove(RecordToDelete);
      dbContext.SaveChanges();
      return RedirectToAction("ShowHome");
    }
  }
}