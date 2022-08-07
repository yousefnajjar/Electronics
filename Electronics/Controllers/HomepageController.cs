using Electronics.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Electronics.Controllers
{
    public class HomepageController : Controller
    {

        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public HomepageController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;

        }

        public async void AddToCart(int ProductId, float ProductPrice)
        {
            OrderProduct orderProduct = new OrderProduct();
            Order order = new Order();

            if (ProductId != 0)
            {
                order.UserId = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
                order.Status = "No";
                order.Quntity = 1;
                order.TotalAmount = (decimal)ProductPrice;
                order.OrderDate = DateTime.Now;
                
                _context.Add(order);
                await _context.SaveChangesAsync();

                orderProduct.OrderId = order.OrderId;
                orderProduct.ProductId = ProductId;

                _context.Add(orderProduct);
                await _context.SaveChangesAsync();
            }
        }

        //**************
        public IActionResult Index(int ProductId = 0, float ProductPrice = 0)
        {


            
            ViewBag.userId = HttpContext.Session.GetString("UserID");
            dynamic model = new ExpandoObject();
            model.Category = _context.Categories.ToList();
            model.Product = _context.Products.ToList();


            model.Address = _context.Websites.Select(x => x.Address).SingleOrDefault();
            model.Phone = _context.Websites.Select(x => x.Phone).SingleOrDefault();
            model.Email = _context.Websites.Select(x => x.Email).SingleOrDefault();

            model.websiteName = _context.Websites.Select(x => x.WebName).SingleOrDefault();
            model.WebImagelogoPath = _context.Websites.Select(x => x.WebImagelogoPath).SingleOrDefault();
            model.WebImagePackgroundPath = _context.Websites.Select(x => x.WebImagePackgroundPath).SingleOrDefault();


            model.Info = _context.AboutUs.Select(x => x.Info).SingleOrDefault();
            model.AboutImage = _context.AboutUs.Select(x => x.ImagePath).SingleOrDefault();

            if (HttpContext.Session.GetString("UserID") != null)
            {
                model.userEmail = _context.UserLogins.Where(x => x.UserId.ToString() ==
                HttpContext.Session.GetString("UserID")).Select(x => x.UserEmail).SingleOrDefault();

                model.userImage = _context.UserLogins.Where(x => x.UserId.ToString() ==
                HttpContext.Session.GetString("UserID")).Select(x => x.UserImagepath).SingleOrDefault();
            }
            else
            {
                model.userEmail = null;
                model.userImage = null;
            }
            AddToCart(ProductId, ProductPrice);
            return View(model);
        }


        public IActionResult contact()
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["WebId"] = new SelectList(_context.Websites, "WebId", "WebId", contactU.WebId);
            return View(contactU);
        }

        public async Task<IActionResult> EditCusProfile(decimal? id)
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
            ViewBag.websiteName = _context.Websites.Select(x => x.WebName).SingleOrDefault();
            ViewBag.WebImageLogoPath = _context.Websites.Select(x => x.WebImagelogoPath).SingleOrDefault();

            ViewBag.WebImagePackgroundPath = _context.Websites.Select(x => x.WebImagePackgroundPath).SingleOrDefault();
            ViewBag.Address = _context.Websites.Select(x => x.Address).SingleOrDefault();
            ViewBag.Phone = _context.Websites.Select(x => x.Phone).SingleOrDefault();
            ViewBag.Email = _context.Websites.Select(x => x.Email).SingleOrDefault();
            ViewBag.AboutUs = _context.AboutUs.Select(x => x.Info).SingleOrDefault();
            ViewBag.BackgroundImage = _context.AboutUs.Select(x => x.ImagePath).SingleOrDefault();
            
            ViewData["RoleName"] = new SelectList(_context.Roles, "RoleId", "RoleName");
            return View(userLogin);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCusProfile(decimal id, [Bind("UserId,Fname,Lname,UserEmail,UserPassword,Address,UserImagepath,Salary,ImageFile,RoleId")] UserLogin userLogin)
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
                return RedirectToAction("Index", "Homepage");
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
