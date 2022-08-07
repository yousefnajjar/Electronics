using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Electronics.Models;
using Microsoft.AspNetCore.Http;

namespace Electronics.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly ModelContext _context;

        public ContactUsController(ModelContext context)
        {
            _context = context;
        }

        // GET: ContactUs
        public async Task<IActionResult> Index()
        { 
            
            
            //var auth1 = _context.UserLogins.Where(x => x.UserId.ToString()
            //== HttpContext.Session.GetString("UserID")).Select(x => x.RoleId).SingleOrDefault();
            //ViewBag.userId = auth1.ToString();


            var modelContext = _context.ContactU.Include(c => c.Web);

            return View(await modelContext.ToListAsync());
        }
/// <summary>
/// ///////////////////////////////////////////////////Acc
/// </summary>
/// <returns></returns>
        public async Task<IActionResult> ACCIndex()
        {

            var modelContext = _context.ContactU.Include(c => c.Web);
            
            return View(await modelContext.ToListAsync());
        }


        // GET: ContactUs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactU = await _context.ContactU
                .Include(c => c.Web)
                .FirstOrDefaultAsync(m => m.ContactUsId == id);
            if (contactU == null)
            {
                return NotFound();
            }

            return View(contactU);
        }

        // GET: ContactUs/Create
        public IActionResult Create()
        {
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId");
            return View();
        }

        // POST: ContactUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactUsId,Fname,Lname,Phone,Message,WebId")] ContactU contactU)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactU);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Homepage");
            }
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId", contactU.WebId);
            return View(contactU);
        }

        // GET: ContactUs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactU = await _context.ContactU.FindAsync(id);
            if (contactU == null)
            {
                return NotFound();
            }
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId", contactU.WebId);
            return View(contactU);
        }

        // POST: ContactUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("ContactUsId,Fname,Lname,Phone,Message,WebId")] ContactU contactU)
        {
            if (id != contactU.ContactUsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactU);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactUExists(contactU.ContactUsId))
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
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId", contactU.WebId);
            return View(contactU);
        }

        // GET: ContactUs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactU = await _context.ContactU
                .Include(c => c.Web)
                .FirstOrDefaultAsync(m => m.ContactUsId == id);
            if (contactU == null)
            {
                return NotFound();
            }

            return View(contactU);
        }

        // POST: ContactUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var contactU = await _context.ContactU.FindAsync(id);
            _context.ContactU.Remove(contactU);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactUExists(decimal id)
        {
            return _context.ContactU.Any(e => e.ContactUsId == id);
        }
    }
}
