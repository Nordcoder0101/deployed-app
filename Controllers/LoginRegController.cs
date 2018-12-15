using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using beltexam.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Data.Entity;

namespace beltexam.Controllers
{
  public class LoginRegController : Controller
  {
    private beltexamContext dbContext;
    
    public LoginRegController(beltexamContext context)
    {
      dbContext = context;
    
    }
    
    [HttpGet]
    [Route("")]
    public IActionResult Index()
    {
      return View();
    }

    [HttpPost("RegisterUser")]
    public IActionResult RegisterUser(User user)
    {
      if (ModelState.IsValid)
      {
        if (dbContext.Users.Any(u => u.Email == user.Email))
        {
          ModelState.AddModelError("Email", "Email already in use!");
          return View("Index");
        }
        else
        {
          PasswordHasher<User> Hasher = new PasswordHasher<User>();
          user.Password = Hasher.HashPassword(user, user.Password);

          dbContext.Add(user);
          dbContext.SaveChanges();
          var AddedUser = dbContext.Users.FirstOrDefault(u => u.Email == user.Email);
          HttpContext.Session.SetInt32("UserId", AddedUser.UserId);
          int? LoggedInUserId = HttpContext.Session.GetInt32("UserId");

          return RedirectToAction("ShowHome", "UserAndActivity");
        }
      }
      else
      {
        return View("Index");
      }
    }

    [HttpGet("ViewLoginUser")]
    public IActionResult ViewLoginUser()
    {
      return View();
    }

    [HttpPost("LoginUser")]
    public IActionResult LoginUser(LoginUser UserSubmission)
    {
      if (ModelState.IsValid)
      {
        var UserToCheck = dbContext.Users.FirstOrDefault(u => u.Email == UserSubmission.Email);
        if (UserToCheck == null)
        {
          ModelState.AddModelError("Email", "Invalid Email or Password");
          return View("Index");
        }
        var hasher = new PasswordHasher<LoginUser>();

        var result = hasher.VerifyHashedPassword(UserSubmission, UserToCheck.Password, UserSubmission.Password);

        if (result == 0)
        {
          ModelState.AddModelError("Password", "Invalid Email or Password");
          return View("Index");
        }
        HttpContext.Session.SetInt32("UserId", UserToCheck.UserId);
        int? LoggedInUserId = HttpContext.Session.GetInt32("UserId");
        return RedirectToAction("ShowHome", "UserAndActivity");
      }
      return View("Index");
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
      HttpContext.Session.Clear();
      return View("Index");
    }
  }


}

