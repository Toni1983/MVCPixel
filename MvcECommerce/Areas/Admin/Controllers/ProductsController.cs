using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcECommerce.Models;
using SummerCampECommerce2019.Models;

namespace MvcECommerce.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private ECommerce_2019_DbEntities db = new ECommerce_2019_DbEntities();

        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Brands).Include(p => p.Categories).Include(p => p.ProductDetails);
            return View(products.ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Name");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "BrandId,CategoryId,Name,Price,IsNew,IsSale,IsActive,ProductDetails")] Products products,
            HttpPostedFileBase file, string editor1, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                ImageUpload imageUpload = new ImageUpload();
                if (file != null)
                {
                    products.ImageURL = imageUpload.ImageResize(file, 255, 237);
                }
                products.ProductDetails.Description = editor1;
                products.RegisterDate = DateTime.Now;
                if (files.FirstOrDefault() != null)
                {
                    foreach (var item in files)
                    {
                        var paths = imageUpload.ImageResize(item, 84, 84, 329, 380);
                        products.ProductDetails.Images.Add(new Images
                        {
                            ImageURL = paths.Item1,
                            ImageURLt = paths.Item2
                        });
                    }
                }
                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Name", products.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", products.CategoryId);
            ViewBag.ProductId = new SelectList(db.ProductDetails, "ProductId", "Description", products.ProductId);
            return View(products);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Name", products.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", products.CategoryId);
            ViewBag.ProductId = new SelectList(db.ProductDetails, "ProductId", "Description", products.ProductId);
            return View(products);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,BrandId,CategoryId,Name,Price,IsNew,IsSale,IsActive,ProductDetails")] Products products)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Name", products.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", products.CategoryId);
            ViewBag.ProductId = new SelectList(db.ProductDetails, "ProductId", "Description", products.ProductId);
            return View(products);
        }

        public ActionResult DeleteImages(int imageId, int id)
        {
            db.Images.Remove(db.Products.Find(id).ProductDetails.Images.FirstOrDefault(x => x.ImageId == imageId));
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = id });
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
