using Electronics.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Electronics.Controllers
{
    public class LoginAndRegisterController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public LoginAndRegisterController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;

        }
       
        public IActionResult Register()
        {

            ViewBag.websiteName = _context.Websites.Select(x => x.WebName).SingleOrDefault();
            ViewBag.WebImageLogoPath = _context.Websites.Select(x => x.WebImagelogoPath).SingleOrDefault();
            ViewBag.WebImagePackgroundPath = _context.Websites.Select(x => x.WebImagePackgroundPath).SingleOrDefault();
            ViewBag.Address = _context.Websites.Select(x => x.Address).SingleOrDefault();
            ViewBag.Phone = _context.Websites.Select(x => x.Phone).SingleOrDefault();
            ViewBag.Email = _context.Websites.Select(x => x.Email).SingleOrDefault();
            ViewBag.AboutUs = _context.AboutUs.Select(x => x.Info).SingleOrDefault();
            ViewBag.BackgroundImage = _context.AboutUs.Select(x => x.ImagePath).SingleOrDefault();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserId,Fname,Lname,UserEmail,UserPassword,Address,UserImagepath,ImageFile,RoleId")] UserLogin userLogin)
        {//, string username, string pass
            if (ModelState.IsValid)
            {
                if (userLogin.ImageFile != null)
                {
                    string webrootpath = _webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + userLogin.ImageFile.FileName;
                    string path = Path.Combine(webrootpath + "/Image/" + fileName);

                    using (var streamfile = new FileStream(path, FileMode.Create))
                    {
                        await userLogin.ImageFile.CopyToAsync(streamfile);
                    }
                    //assgin value of file name to image path
                    userLogin.UserImagepath = fileName;
                    //add new reg customer
                    HttpContext.Session.SetString("UserID", userLogin.UserId.ToString());

                    userLogin.RoleId = 2;
                    _context.Add(userLogin);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login", "LoginAndRegister");
                }
            }
            return View();
        }
        public IActionResult Login()
        {

            ViewBag.websiteName = _context.Websites.Select(x => x.WebName).SingleOrDefault();
            ViewBag.WebImageLogoPath = _context.Websites.Select(x => x.WebImagelogoPath).SingleOrDefault();
            ViewBag.WebImagePackgroundPath = _context.Websites.Select(x => x.WebImagePackgroundPath).SingleOrDefault();
            ViewBag.Address = _context.Websites.Select(x => x.Address).SingleOrDefault();
            ViewBag.Phone = _context.Websites.Select(x => x.Phone).SingleOrDefault();
            ViewBag.Email = _context.Websites.Select(x => x.Email).SingleOrDefault();
            ViewBag.AboutUs = _context.AboutUs.Select(x => x.Info).SingleOrDefault();
            ViewBag.BackgroundImage = _context.AboutUs.Select(x => x.ImagePath).SingleOrDefault();

            return View();
        }
        //to compare data that enter from user with data that already stored in DB
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("UserEmail , UserPassword")] UserLogin userLogin)
        {
            var auth = _context.UserLogins.Where(x => x.UserEmail == userLogin.UserEmail && x.UserPassword == userLogin.UserPassword).SingleOrDefault();

            

            if (auth != null)
            {
                HttpContext.Session.SetString("UserID", auth.UserId.ToString());

                switch (auth.RoleId)
                {
                    case 0: //Admin
                        return RedirectToAction("Employee", "Home");
                    case 1: //Accountant
                        return RedirectToAction("AccHome", "Accountant");
                    case 2: //Customer
                        return RedirectToAction("Index", "Homepage");
                }
            }
            else
            { return RedirectToAction("Login", "LoginAndRegister");}
            return View();
        }

    }
}
