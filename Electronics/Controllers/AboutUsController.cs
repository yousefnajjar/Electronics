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
    public class AboutUsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public AboutUsController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;

        }

        // GET: AboutUs
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.AboutUs.Include(a => a.Web);
            return View(await modelContext.ToListAsync());
        }

        // GET: AboutUs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutU = await _context.AboutUs
                .Include(a => a.Web)
                .FirstOrDefaultAsync(m => m.AboutUsId == id);
            if (aboutU == null)
            {
                return NotFound();
            }

            return View(aboutU);
        }

        // GET: AboutUs/Create
        public IActionResult Create()
        {
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId");
            return View();
        }

        // POST: AboutUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AboutUsId,Info,ImagePath,WebId")] AboutU aboutU)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aboutU);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId", aboutU.WebId);
            return View(aboutU);
        }

        // GET: AboutUs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutU = await _context.AboutUs.FindAsync(id);
            if (aboutU == null)
            {
                return NotFound();
            }
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId", aboutU.WebId);
            return View(aboutU);
        }

        // POST: AboutUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("AboutUsId,Info,ImagePath,ImageFile,WebId")] AboutU aboutU)
        {
            if (id != aboutU.AboutUsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (aboutU.ImageFile != null)
                    {
                        string wwwrootPath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + aboutU.ImageFile.FileName;
                        string path = Path.Combine(wwwrootPath + "/Image/" + fileName);
                        using (var streamfile = new FileStream(path, FileMode.Create))
                        {
                            await aboutU.ImageFile.CopyToAsync(streamfile);
                        }
                        //assgin value of file name to image path
                        aboutU.ImagePath = fileName;
                        _context.Update(aboutU);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                  
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutUExists(aboutU.AboutUsId))
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
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId", aboutU.WebId);
            return View(aboutU);
        }

        // GET: AboutUs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutU = await _context.AboutUs
                .Include(a => a.Web)
                .FirstOrDefaultAsync(m => m.AboutUsId == id);
            if (aboutU == null)
            {
                return NotFound();
            }

            return View(aboutU);
        }

        // POST: AboutUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var aboutU = await _context.AboutUs.FindAsync(id);
            _context.AboutUs.Remove(aboutU);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutUExists(decimal id)
        {
            return _context.AboutUs.Any(e => e.AboutUsId == id);
        }
    }
}
