using MvcECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;

namespace MvcECommerce.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        ECommerce_2019_DbEntities2 db = new ECommerce_2019_DbEntities2();
        multipleModel vm = new multipleModel();
        public ActionResult Index(int? id, int? UserId)
        {
            dynamic dy = new ExpandoObject();
            dy.categories = vm.GetCategories();
            dy.products = vm.FilterProducts(id);
            dy.brands = vm.GetBrands();
            dy.currentUser = vm.GetUserId(UserId);
            if (UserId != null)
            {
                var currentUser = vm.GetUserId(UserId);
                ViewBag.currentUser = currentUser.UserName;
            }
            else
            {
                ViewBag.text = "Login";
            }
            return View(dy);
        }
        public ActionResult Filter(int id)
        {
            dynamic dy = new ExpandoObject();
            dy.categories = vm.GetCategories();
            dy.products = vm.FilterProducts(id);
            dy.brands = vm.GetBrands();
            return RedirectToAction("Index", new { id = id });
        }
        public ActionResult ProductDetails(int id)
        {
            dynamic dy = new ExpandoObject();
            dy.categories = vm.GetCategories();
            dy.brands = vm.GetBrands();
            dy.images = vm.FilterImages(id);
            dy.productId = vm.GetId(id);
            return View(dy);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Email, string Password, Users User)
        {
            if (db.Users.Any(x => x.Email == Email && x.Password == Password))
            {
                var UserId = db.Users.First(x => x.Email == Email && x.Password == Password).UserId;
                return RedirectToAction("Index", new { UserId });
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
                return RedirectToAction("Index", new { User.UserId });
            }
            else
            {
                ViewBag.IncorrectPassword = "Please enter the same Password";
                return RedirectToAction("Login");
            }
        }
        public ActionResult AddToWishlist(int id,int UserId , WishList wishList)
        {
            var productId = db.Products.Find(id);
            var userId = db.Users.Find(UserId);
            wishList.IsActive = true;
            db.WishList.Add(wishList);
            db.SaveChanges();
            return RedirectToAction("Index", new { UserId = userId.UserId });
        }
        public ActionResult DisplayWishList(int UserId)
        {
            List<WishList> wishList = db.WishList.OrderBy(x => x.UserId == UserId).ToList();
            return RedirectToAction("Index", new { wishList, UserId = UserId });
        }
    }
}