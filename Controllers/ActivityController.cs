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
  public class ActivityController : Controller
  {
    private beltexamContext dbContext;

    public ActivityController(beltexamContext context)
    {
      dbContext = context;

    }
    [HttpGet("showcreateactivity")]
    public IActionResult ShowCreateActivity()
    {
      if (HttpContext.Session.GetInt32("UserId") == null)
      {
        return View("Index");
      }
      int? LoggedInUserId = HttpContext.Session.GetInt32("UserId");
      ViewBag.LoggedInUserId = LoggedInUserId;
      var LoggedInUser = dbContext.Users.FirstOrDefault(u => u.UserId == LoggedInUserId);
      ViewBag.LoggedInUser = LoggedInUser;

      return View();
    }

    [HttpPost("CreateActivity")]
    public IActionResult CreateActivity(Activity NewActivity, double Hour, double Minute, string ampm, string FirstName, string LastName)
    {

      if(ModelState.IsValid)
      {
        NewActivity.CreatorName = FirstName + " " + LastName;
        
        if (ampm == "am")
        {
          Hour += 12;
        }
        NewActivity.Date = NewActivity.Date.AddHours(Hour).AddMinutes(Minute);
        if (NewActivity.Date < DateTime.Now)
        {
          ModelState.AddModelError("Date", "Date Must be after today");
          return View("ShowCreateActivity");
        }
        dbContext.Add(NewActivity);
        dbContext.SaveChanges();
        return RedirectToAction("ShowHome", "UserAndActivity");

      }
      int? LoggedInUserId = HttpContext.Session.GetInt32("UserId");
      ViewBag.LoggedInUserId = LoggedInUserId;
      var LoggedInUser = dbContext.Users.FirstOrDefault(u => u.UserId == LoggedInUserId);
      ViewBag.LoggedInUser = LoggedInUser;
      
      
      return View("ShowCreateActivity");
    }

    [HttpGet("deleteactivity/{id}")]
    public IActionResult DeleteActivity(int id)
    {
      var ActivityToDelete = dbContext.Activities.FirstOrDefault(a => a.ActivityId == id);
      dbContext.Remove(ActivityToDelete);
      dbContext.SaveChanges();
      return RedirectToAction("ShowHome", "UserAndActivity");
    }

    [HttpGet("activity/{id}")]
    public IActionResult ShowActivity(int id)
    {
      if (HttpContext.Session.GetInt32("UserId") == null)
      {
        return View("Index");
      }

      int? LoggedInUserId = HttpContext.Session.GetInt32("UserId");
      var LoggedInUser = dbContext.Users.FirstOrDefault(u => u.UserId == LoggedInUserId);
      ViewBag.LoggedInUser = LoggedInUser;

      var ActivityToShow = dbContext.Activities.FirstOrDefault(a => a.ActivityId == id);
      ViewBag.ActivityToShow = ActivityToShow;

      var AllActivities = dbContext.Activities
      .Include(uaa => uaa.UserAndActivities)
      .ThenInclude(u => u.User).ToList();

      ViewBag.AllActivities = AllActivities;
      
      return View();
    }
  }
}