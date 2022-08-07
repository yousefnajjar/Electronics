using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Electronics.Models;
using System.Dynamic;
using Microsoft.AspNetCore.Http;

namespace Electronics.Controllers
{
    public class CartsController : Controller
    {
        private readonly ModelContext _context;

        public CartsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Carts
        public IActionResult Index()
        {
            dynamic model = new ExpandoObject();
            List<Order> orders = new List<Order>();
            List<OrderProduct> orderProduct = new List<OrderProduct>();
            List<Product> products = new List<Product>();

            orders = _context.Orders.Where(x => x.UserId.ToString()
            == HttpContext.Session.GetString("UserID")).ToList();

            for (int i = 0; i < orders.Count; i++)
            {
                orderProduct.Add(_context.OrderProducts.Where(x => x.OrderId == orders[i].OrderId).SingleOrDefault());
            }
            for (int i = 0; i < orderProduct.Count; i++)
            {
                products.Add(_context.Products.Where(x => x.ProductId == orderProduct[i].ProductId).SingleOrDefault());
            }

            model.orders = orders;
            model.products = products;



            ViewBag.websiteName = _context.Websites.Select(x => x.WebName).SingleOrDefault();
            ViewBag.WebImageLogoPath = _context.Websites.Select(x => x.WebImagelogoPath).SingleOrDefault();
            ViewBag.WebImagePackgroundPath = _context.Websites.Select(x => x.WebImagePackgroundPath).SingleOrDefault();
            ViewBag.Address = _context.Websites.Select(x => x.Address).SingleOrDefault();
            ViewBag.Phone = _context.Websites.Select(x => x.Phone).SingleOrDefault();
            ViewBag.Email = _context.Websites.Select(x => x.Email).SingleOrDefault();
            ViewBag.AboutUs = _context.AboutUs.Select(x => x.Info).SingleOrDefault();
            ViewBag.BackgroundImage = _context.AboutUs.Select(x => x.ImagePath).SingleOrDefault();
            return View(model);
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Card)
                .Include(c => c.Order)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            ViewData["CardId"] = new SelectList(_context.Cards, "CardId", "CardId");
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            ViewData["UserId"] = new SelectList(_context.UserLogins, "UserId", "UserId");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId,UserId,OrderId,CardId")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CardId"] = new SelectList(_context.Cards, "CardId", "CardId", cart.CardId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", cart.OrderId);
            ViewData["UserId"] = new SelectList(_context.UserLogins, "UserId", "UserId", cart.UserId);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["CardId"] = new SelectList(_context.Cards, "CardId", "CardId", cart.CardId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", cart.OrderId);
            ViewData["UserId"] = new SelectList(_context.UserLogins, "UserId", "UserId", cart.UserId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("CartId,UserId,OrderId,CardId")] Cart cart)
        {
            if (id != cart.CartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.CartId))
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
            ViewData["CardId"] = new SelectList(_context.Cards, "CardId", "CardId", cart.CardId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", cart.OrderId);
            ViewData["UserId"] = new SelectList(_context.UserLogins, "UserId", "UserId", cart.UserId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Card)
                .Include(c => c.Order)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var cart = await _context.Carts.FindAsync(id);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(decimal id)
        {
            return _context.Carts.Any(e => e.CartId == id);
        }
    }
}
