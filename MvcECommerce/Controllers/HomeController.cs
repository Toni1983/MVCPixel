using MvcECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcECommerce.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        ECommerce_2019_DbEntities2 db = new ECommerce_2019_DbEntities2();
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }
    }
}