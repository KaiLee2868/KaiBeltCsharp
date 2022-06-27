#pragma warning disable CS8618
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KaiBeltCsharp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace KaiBeltCsharp.Controllers;

public class HomeController : Controller
{
    private MyContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult Index()
    {       
        int? session = HttpContext.Session.GetInt32("loggedinUser");
        

        if (session != null)
        {

            return RedirectToAction("Dashboard");
        }
        return View();
    }
    [HttpPost("register")]
    public IActionResult Register(User user)
    {
        ViewBag.Notlogin = true;

        if (ModelState.IsValid)
        {

            if (_context.Users.Any(u => u.Email == user.Email))
            {
                ModelState.AddModelError("Email", "Email already in use!");

                return View("Index");
            }

            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            user.Password = Hasher.HashPassword(user, user.Password);
            _context.Add(user);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("loggedinUser", user.UserId);
            return RedirectToAction("Dashboard");


        }

        return View("Index");

    }
    [HttpPost("login")]
    public IActionResult Login(LoginUser newuser)
    {
        ViewBag.Notlogin = true;

        if (ModelState.IsValid)
        {

            var userInDb = _context.Users.FirstOrDefault(u => u.Email == newuser.LogEmail);

            if (userInDb == null)
            {
                ModelState.AddModelError("LogEmail", "Invalid Email/Password");
                return View("Index");
            }

            PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();

            var result = hasher.VerifyHashedPassword(newuser, userInDb.Password, newuser.LogPassword);

            if (result == 0)
            {
                ModelState.AddModelError("LogPassword", "Incorrect Password");
                return View("Index");
            }

            HttpContext.Session.SetInt32("loggedinUser", userInDb.UserId);
            return RedirectToAction("Dashboard");

        }
        return View("Index");
    }
    [HttpGet("dashboard")]
    public IActionResult Dashboard(int UserId)
    {
        ViewBag.Notlogin = false;
        int? session = HttpContext.Session.GetInt32("loggedinUser");
        if (session == null)
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
        ViewBag.CurrentUser = _context.Users.FirstOrDefault(i => i.UserId == (int)session);
        ViewBag.AllMeeting = _context.Meets.Include(i => i.Creator).Include(i => i.Guests).OrderBy(d=>d.DateandTime);
        List<User> users = _context.Users.ToList();
        ViewBag.username = users;

        return View("Dashboard");
    }

    [HttpGet("meetups/new/{UserId}")]
    public IActionResult newPlan(int UserId)   

    {        
        ViewBag.Notlogin = false;
        int? session = HttpContext.Session.GetInt32("loggedinUser");


        if (session == null)
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

        return View("newPlan");
    }
    [HttpPost("plan")]
    public IActionResult Plan(Meet meet)    
    {
        ViewBag.Notlogin = false;
        int? session = HttpContext.Session.GetInt32("loggedinUser");
        if (ModelState.IsValid)
        {

            meet.UserId = (int)session;
            _context.Add(meet);
            _context.SaveChanges();

            return RedirectToAction("Meeting", new{meet.MeetId});
        }
        else
        {
            return View("newPlan");
        }

    }

    [HttpGet("meetup/{MeetId}")]
    public IActionResult Meeting(int MeetId)
    {
        ViewBag.Notlogin = false;
        int? session = HttpContext.Session.GetInt32("loggedinUser");

        if (session == null)
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
        ViewBag.CurrentUser = _context.Users.FirstOrDefault(i => i.UserId == (int)session);
    
        ViewBag.AllGuests = _context.Parties.Include(i => i.Guest).Where(i => i.MeetId == MeetId);
        ViewBag.PlannedMeets = _context.Meets.FirstOrDefault(i => i.MeetId == MeetId);
        Meet meets = _context.Meets.FirstOrDefault(m => m.MeetId == MeetId); 
        ViewBag.des = meets;       
        ViewBag.Coordinator = _context.Users.FirstOrDefault(u => u.UserId == meets.UserId);
        
        
        return View("Meeting");
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {    
        HttpContext.Session.Clear();
        return RedirectToAction("Dashboard");
    }

    [HttpGet("delete/{MeetId}")]
    public IActionResult Delete(int MeetId)
    {
        ViewBag.Notlogin = false;
        Meet? MeetToDelete = _context.Meets.SingleOrDefault(i => i.MeetId == MeetId);

        _context.Remove(MeetToDelete);
        _context.SaveChanges();
        return RedirectToAction("Dashboard");
    }

    [HttpGet("join/{MeetId}")]
    public IActionResult Join(int MeetId)
    {
        int? session = HttpContext.Session.GetInt32("loggedinUser");
        Party guest = new Party();
        guest.UserId = (int)session;
        guest.MeetId = MeetId;

        _context.Add(guest);
        _context.SaveChanges();


        return RedirectToAction("Dashboard");
    }

    [HttpGet("leave/{MeetId}")]
    public IActionResult Leave(int MeetId)
    {
        int? session = HttpContext.Session.GetInt32("loggedinUser");
        Party? guestToRemove = _context.Parties.Where(i => i.MeetId == MeetId).SingleOrDefault(i => i.UserId == (int)session);


        _context.Remove(guestToRemove);
        _context.SaveChanges();


        return RedirectToAction("Dashboard");
    }

}

