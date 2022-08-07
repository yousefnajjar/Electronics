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
    public class HomeController : Controller
    {

        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public HomeController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;

        }

  
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Report()
        {
              var userSalary = _context.UserLogins.Select(x => x.Salary).Sum();
              var orderSold = _context.Orders.Select(x => x.TotalAmount).Sum();
                dynamic model = new ExpandoObject(); 
                model.userSalary = userSalary;
                model.orderSold = orderSold;
            return View(model);
        }
      
      
        //Employee Crud**********************************************************************************************
        public IActionResult Employee()
        {
            var auth = _context.UserLogins.ToList();
            List<UserLogin> emp = new List<UserLogin>();

            for (int i = 0; i < auth.Count; i++)
            {
                if (auth[i].RoleId == 0 || auth[i].RoleId == 1 )
                    emp.Add(auth[i]);
            }
            return View(emp);
        }
//        for (int i = 0; i<auth.Count; i++)
//            {
//                var position = _context.Roles.Where(a => a.RoleId == auth[i].RoleId).Select(x => x.RoleName);
//                if (auth[i].RoleId != 2)
//                {
//                    auth[i].Positon = position.FirstOrDefault();
//                    emp.Add(auth[i]);
//                }
//}

public IActionResult UserView()
        {
            var auth = _context.UserLogins.ToList();
            List<UserLogin> users = new List<UserLogin>();
            for (int i = 0; i < auth.Count; i++)
            {
                if (auth[i].RoleId == 2)
                    users.Add(auth[i]);
            }
            
            return View(users);
        }
        public async Task<IActionResult> EmployeeDelete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userLogin = await _context.UserLogins
            .Include(u => u.Role)
            .FirstOrDefaultAsync(m => m.UserId == id);
            if (userLogin == null)
            {
                return NotFound();
            }
            return View(userLogin);
        }
        // POST: UserLogins/Delete/5
        [HttpPost, ActionName("EmployeeDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeDelete(decimal id)
        {
            var userLogin = await _context.UserLogins.FindAsync(id);
            _context.UserLogins.Remove(userLogin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Employee));
        }



        public async Task<IActionResult> EmployeeEdit(decimal? id)
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
            ViewData["RoleName"] = new SelectList(_context.Roles, "RoleId", "RoleName");
            return View(userLogin);
        }



        // POST: UserLogins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeEdit(decimal id, [Bind("UserId,Fname,Lnmae,UserEmail,UserPassword,Address,UserImagepath,ImageFile,RoleId,Salary")] UserLogin userLogin)
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
                return RedirectToAction(nameof(Employee));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", userLogin.RoleId);
            return View(userLogin);
        }



        


        public IActionResult EmployeeCreate()
        {
           
            ViewData["RoleName"] = new SelectList(_context.Roles, "RoleId", "RoleName");
            return View();
        }



      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeCreate([Bind("UserId,Fname,Lname,UserEmail,UserPassword,Address,UserImagepath,ImageFile,RoleId,Salary")] UserLogin userLogin)
        {
            if (ModelState.IsValid)
            {
                if (userLogin.ImageFile != null)
                {
                    string wwwrootPath = _webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + userLogin.ImageFile.FileName;
                    string path = Path.Combine(wwwrootPath + "/Image/" + fileName);
                    using (var streamfile = new FileStream(path, FileMode.Create))
                    {
                        await userLogin.ImageFile.CopyToAsync(streamfile);
                    }
                    userLogin.UserImagepath = fileName;
                    _context.Add(userLogin);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Employee));
                }
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", userLogin.Role);
            return View(userLogin);
        }







        public async Task<IActionResult> UserDelete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userLogin = await _context.UserLogins
            .Include(u => u.Role)
            .FirstOrDefaultAsync(m => m.UserId == id);
            if (userLogin == null)
            {
                return NotFound();
            }
            return View(userLogin);
        }
        [HttpPost, ActionName("UserDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserDelete(decimal id)
        {
            var userLogin = await _context.UserLogins.FindAsync(id);
            _context.UserLogins.Remove(userLogin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(UserView));
        }


        private bool UserLoginExists(decimal id)
        {
            return _context.UserLogins.Any(e => e.UserId == id);
        }

        


    }



}

