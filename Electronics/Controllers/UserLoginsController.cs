using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Electronics.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Electronics.Controllers
{
    public class UserLoginsController : Controller
    {

        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public UserLoginsController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;

        }
        // GET: UserLogins
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.UserLogins.Include(u => u.Role);
            return View(await modelContext.ToListAsync());
        }

        // GET: UserLogins/Details/5
        public async Task<IActionResult> Details(decimal? id)
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

        // GET: UserLogins/Create
        public IActionResult Create()
        {
            ViewData["RoleName"] = new SelectList(_context.Roles, "RoleId", "RoleName");
            return View();
        }

        // POST: UserLogins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Fname,Lname,UserEmail,UserPassword,Address,UserImagepath,ImageFile,RoleId,Salary")] UserLogin userLogin)
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
                    //assgin value of file name to image path
                    userLogin.UserImagepath = fileName;
                    _context.Add(userLogin);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", userLogin.RoleId);
            return View(userLogin);
        }

        // GET: UserLogins/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLogin = await _context.UserLogins.FindAsync(id);
            //var userLogin = await _context.UserLogins.FirstOrDefaultAsync(m => m.UserId == id);
            if (userLogin == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", userLogin.RoleId);
            return View(userLogin);
        }

        // POST: UserLogins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("UserId,Fname,Lname,UserEmail,UserPassword,Address,UserImagepath,ImageFile,RoleId,Salary")] UserLogin userLogin)
        {
            if (id != userLogin.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
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
                        //assgin value of file name to image path
                        userLogin.UserImagepath = fileName;
                        _context.Add(userLogin);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    //_context.Update(userLogin);
                    //await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", userLogin.RoleId);
            return View(userLogin);
        }

        // GET: UserLogins/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var userLogin = await _context.UserLogins.FindAsync(id);
            _context.UserLogins.Remove(userLogin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserLoginExists(decimal id)
        {
            return _context.UserLogins.Any(e => e.UserId == id);
        }
    }
}
