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
        public ActionResult Index(int? id)
        {
            dynamic dy = new ExpandoObject();
            dy.categories = vm.GetCategories();
            dy.products = vm.FilterProducts(id);
            dy.brands = vm.GetBrands();
            return View(dy);
        }
        public ActionResult Filter(int id)
        {
            dynamic dy = new ExpandoObject();
            dy.categories = vm.GetCategories();
            dy.products = vm.FilterProducts(id);
            dy.brands = vm.GetBrands();
            return RedirectToAction("Index",new { id = id});
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
        

    }
}