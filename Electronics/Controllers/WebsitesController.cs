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
    public class WebsitesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;
        public WebsitesController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;

        }

        // GET: Websites
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Websites.Include(w => w.Card);
            return View(await modelContext.ToListAsync());
        }

        // GET: Websites/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var website = await _context.Websites
                .Include(w => w.Card)
                .FirstOrDefaultAsync(m => m.WebId == id);
            if (website == null)
            {
                return NotFound();
            }

            return View(website);
        }

        // GET: Websites/Create
        public IActionResult Create()
        {
            ViewData["CardId"] = new SelectList(_context.Cards, "CardId", "CardId");
            return View();
        }

        // POST: Websites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WebId,WebName,WebImagelogoPath,WebImagePackgroundPath,ImageLogo,ImageBackground,Address,Phone,Email,CardId")] Website website)
        {
            if (ModelState.IsValid)
            {
                _context.Add(website);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CardId"] = new SelectList(_context.Cards, "CardId", "CardId", website.CardId);
            return View(website);
        }

        // GET: Websites/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var website = await _context.Websites.FindAsync(id);
            if (website == null)
            {
                return NotFound();
            }
            ViewData["CardId"] = new SelectList(_context.Cards, "CardId", "CardId", website.CardId);
            return View(website);
        }

        // POST: Websites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("WebId,WebName,WebImagelogoPath,WebImagePackgroundPath,ImageLogo,ImageBackground,Address,Phone,Email,CardId")] Website website)
        {
            if (id != website.WebId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (website.ImageLogo != null && website.ImageBackground != null)
                    {
                        string wwwrootPath1 = _webHostEnviroment.WebRootPath;
                        string fileName1 = Guid.NewGuid().ToString() + "_" + website.ImageLogo.FileName;
                        string path1 = Path.Combine(wwwrootPath1 + "/Image/" + fileName1);

                        string wwwrootPath2 = _webHostEnviroment.WebRootPath;
                        string fileName2 = Guid.NewGuid().ToString() + "_" + website.ImageBackground.FileName;
                        string path2 = Path.Combine(wwwrootPath2 + "/Image/" + fileName2);

                        using (var streamfile1 = new FileStream(path1, FileMode.Create))
                        {
                            await website.ImageLogo.CopyToAsync(streamfile1);
                        }

                        using (var streamfile2 = new FileStream(path2, FileMode.Create))
                        {
                            await website.ImageBackground.CopyToAsync(streamfile2);
                        }
                        website.WebImagelogoPath = fileName1;
                        website.WebImagePackgroundPath = fileName2;

                        _context.Update(website);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index", "Homepage");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebsiteExists(website.WebId))
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
            ViewData["CardId"] = new SelectList(_context.Cards, "CardId", "CardId", website.CardId);
            return View(website);
        }

        // GET: Websites/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var website = await _context.Websites
                .Include(w => w.Card)
                .FirstOrDefaultAsync(m => m.WebId == id);
            if (website == null)
            {
                return NotFound();
            }

            return View(website);
        }

        // POST: Websites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var website = await _context.Websites.FindAsync(id);
            _context.Websites.Remove(website);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebsiteExists(decimal id)
        {
            return _context.Websites.Any(e => e.WebId == id);
        }
    }
}
