using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcECommerce.Models;

namespace MvcECommerce.Areas.Admin.Controllers {
      public class CategoriesController : Controller {
            private ECommerce_2019_DbEntities2 db = new ECommerce_2019_DbEntities2();

            // GET: Admin/Categories
            public ActionResult Index() {
                  return View(db.Categories.ToList());
            }
            public ActionResult ActiveCategories() {
                  return View(db.Categories.Where(x => x.IsActive == true).ToList());
            }
            public ActionResult PassiveCategories() {
                  return View(db.Categories.Where(x => x.IsActive == false).ToList());
            }

            // GET: Admin/Categories/Details/5
            public ActionResult Details(byte? id) {
                  if (id == null) {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                  }
                  Categories categories = db.Categories.Find(id);
                  if (categories == null) {
                        return HttpNotFound();
                  }
                  return View(categories);
            }

            // GET: Admin/Categories/Create
            public ActionResult Create() {
                  return View();
            }

            // POST: Admin/Categories/Create
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "CategoryId,Name,IsActive")] Categories categories) {
                  if (ModelState.IsValid) {
                        db.Categories.Add(categories);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                  }

                  return View(categories);
            }

            // GET: Admin/Categories/Edit/5
            public ActionResult Edit(byte? id) {
                  if (id == null) {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                  }
                  Categories categories = db.Categories.Find(id);
                  if (categories == null) {
                        return HttpNotFound();
                  }
                  return View(categories);
            }

            // POST: Admin/Categories/Edit/5
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "CategoryId,Name,IsActive")] Categories categories) {
                  if (ModelState.IsValid) {
                        db.Entry(categories).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                  }
                  return View(categories);
            }

            // GET: Admin/Categories/Delete/5
            public ActionResult Delete(byte? id) {
                  if (id == null) {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                  }
                  Categories categories = db.Categories.Find(id);
                  if (categories == null) {
                        return HttpNotFound();
                  }
                  return View(categories);
            }

            // POST: Admin/Categories/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(byte id) {
                  Categories categories = db.Categories.Find(id);
                  categories.IsActive = false;
                  db.SaveChanges();
                  return RedirectToAction("Index");
            }

            protected override void Dispose(bool disposing) {
                  if (disposing) {
                        db.Dispose();
                  }
                  base.Dispose(disposing);
            }
      }
}
