using Electronics.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Electronics.Controllers
{
    public class AccountantController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public AccountantController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;

        }




        public IActionResult AccHome() { 
        
        return View();

        }



    public IActionResult ReportAcc()
        {
            var userSalary = _context.UserLogins.Select(x => x.Salary).Sum();
            var orderSold = _context.Orders.Select(x => x.TotalAmount).Sum();
            dynamic model = new ExpandoObject();
            model.userSalary = userSalary;
            model.orderSold = orderSold;

            return View(model);
    
        }


        public async Task<IActionResult> EditAccProfile(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            var userLogin = await _context.UserLogins.FindAsync(id);
            if (userLogin == null)
            {
                return NotFound();
            }
            ViewBag.userId = HttpContext.Session.GetString("UserId");
            dynamic model = new ExpandoObject();
            

            ViewData["RoleName"] = new SelectList(_context.Roles, "RoleId", "RoleName");
            return View(userLogin);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAccProfile(decimal id, [Bind("UserId,Fname,Lnmae,UserEmail,UserPassword,Address,UserImagepath,Salary,ImageFile,RoleId")] UserLogin userLogin)
        {
            if (id != userLogin.UserId)
            {
                return NotFound();
            }



            if (ModelState.IsValid)
            {
                try
                {
                    string wwwrootPath = _webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + userLogin.ImageFile.FileName;
                    string path = Path.Combine(wwwrootPath + "/Image/" + fileName);





                    using (var streamfile = new FileStream(path, FileMode.Create))
                    {
                        await userLogin.ImageFile.CopyToAsync(streamfile);
                    }



                    userLogin.UserImagepath = fileName;



                    _context.Update(userLogin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserLoginExists(userLogin.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("AccHome", "Accountant");
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", userLogin.RoleId);
            return View(userLogin);


        }

        private bool UserLoginExists(decimal id)
        {
            return _context.UserLogins.Any(e => e.UserId == id);
        }



    }
}
