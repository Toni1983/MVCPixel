using MvcECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcECommerce.Models.ViewMoldels;
using System.Web.Routing;

namespace MvcECommerce.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        ECommerce_2019_DbEntities2 db = new ECommerce_2019_DbEntities2();
        public ActionResult Index(HomeViewModel viewModel)
        {
            HttpCookie newCookie = Request.Cookies["User"];
            if (viewModel.WishLists == null || viewModel.Products == null)
            {
                if (newCookie != null)
                {
                    var currentUser = db.Users.Find(Convert.ToInt32(newCookie.Value));
                    ViewBag.currentUser = currentUser.UserName;
                }
                else
                {
                    ViewBag.text = "Login";
                }
                HomeViewModel model = new HomeViewModel();
                model.Categories = db.Categories.ToList();
                model.Brands = db.Brands.ToList();
                model.Products = db.Products.ToList();
                model.Users = db.Users.ToList();
                return View(model);
            }
            if (newCookie.Value != "")
            {
                var currentUser = db.Users.Find(Convert.ToInt32(newCookie.Value));
                ViewBag.currentUser = currentUser.UserName;
            }
            else
            {
                ViewBag.text = "Login";
            }
            
            viewModel.WishLists = db.WishList.Where(a => a.UserId == Convert.ToInt32(newCookie.Value)).ToList();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(int id)
        {
            HomeViewModel model = new HomeViewModel();
            model.Categories = db.Categories.ToList();
            model.Brands = db.Brands.ToList();
            model.Products = db.Products.Where(x => x.CategoryId == id || x.BrandId == id).ToList();
            return View(model);
        }
        public ActionResult ProductDetails(int id)
        {
            HomeViewModel viewModel = new HomeViewModel();
            viewModel.Categories = db.Categories.ToList();
            viewModel.Brands = db.Brands.ToList();
            viewModel.ProductId = db.Products.First(x => x.ProductId == id);
            viewModel.Users = db.Users.ToList();
            return View(viewModel);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            if (db.Users.Any(x => x.Email == Email && x.Password == Password))
            {
                var UserId = db.Users.First(x => x.Email == Email && x.Password == Password).UserId;
                HomeViewModel viewModel = new HomeViewModel();
                HttpCookie userCookie = new HttpCookie("User", UserId.ToString());
                HttpContext.Response.SetCookie(userCookie);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Register = "Please enter the correct code or register yourself first";
                return View();
            }
        }
        public ActionResult Register(string Password, string Verify, Users User)
        {
            if (Password == Verify)
            {
                User.RegisterDate = DateTime.Now;
                User.IsActive = true;
                db.Users.Add(User);
                db.SaveChanges();
                HttpCookie userCookie = new HttpCookie("User", User.UserId.ToString());
                HttpContext.Response.SetCookie(userCookie);
                HttpCookie newCookie = Request.Cookies["User"];
                return RedirectToAction("Index", new { newCookie.Value });
            }
            else
            {
                // Niçin Viewbag buradan göstermiyor?
                ViewBag.IncorrectPassword = "Please enter the same Password";
                return RedirectToAction("Login");
            }
        }
        public ActionResult AddToWishlist(int id, WishList wishList)
        {
            HttpCookie newCookie = Request.Cookies["User"];
            var productId = db.Products.Find(id);
            if (newCookie != null)
            {
                var userId = db.Users.Find(Convert.ToInt32(newCookie.Value));
                wishList.UserId = userId.UserId;
            }
            else
            {
                ViewBag.FirstRegister = "Please register yourself first";
                return RedirectToAction("Index");
            }
            wishList.IsActive = true;
            wishList.ProductId = productId.ProductId;
            db.WishList.Add(wishList);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DisplayWishList()
        {
            HttpCookie newCookie = Request.Cookies["User"];
            HomeViewModel model = new HomeViewModel();
            int newCookieValue = Convert.ToInt32(newCookie.Value);
            model.WishLists = db.WishList.Where(x => x.UserId == newCookieValue).ToList();
            return RedirectToAction("Index", model);
        }
    }
}