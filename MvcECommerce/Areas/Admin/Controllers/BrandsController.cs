﻿using MvcECommerce.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MvcECommerce.Areas.Admin.Controllers {
      public class BrandsController : Controller {
            private ECommerce_2019_DbEntities2 db = new ECommerce_2019_DbEntities2();

            // GET: Admin/Brands
            public ActionResult Index() {
                  return View(db.Brands.ToList());
            }

            public ActionResult ActiveBrands() {
                  return View(db.Brands.Where(x => x.IsActive == true).ToList());
            }

            public ActionResult PassiveBrands() {
                  return View(db.Brands.Where(x => x.IsActive == false).ToList());
            }

            // GET: Admin/Brands/Details/5
            public ActionResult Details(byte? id) {
                  if (id == null) {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                  }
                  Brands brands = db.Brands.Find(id);
                  if (brands == null) {
                        return HttpNotFound();
                  }
                  return View(brands);
            }

            // GET: Admin/Brands/Create
            public ActionResult Create() {
                  return View();
            }

            // POST: Admin/Brands/Create
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "BrandId,Name,IsActive")] Brands brands) {
                  if (ModelState.IsValid) {
                        db.Brands.Add(brands);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                  }

                  return View(brands);
            }

            // GET: Admin/Brands/Edit/5
            public ActionResult Edit(byte? id) {
                  if (id == null) {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                  }
                  Brands brands = db.Brands.Find(id);
                  if (brands == null) {
                        return HttpNotFound();
                  }
                  return View(brands);
            }

            // POST: Admin/Brands/Edit/5
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "BrandId,Name,IsActive")] Brands brands) {
                  if (ModelState.IsValid) {
                        db.Entry(brands).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                  }
                  return View(brands);
            }

            // GET: Admin/Brands/Delete/5
            public ActionResult Delete(byte? id) {
                  if (id == null) {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                  }
                  Brands brands = db.Brands.Find(id);
                  if (brands == null) {
                        return HttpNotFound();
                  }
                  return View(brands);
            }

            // POST: Admin/Brands/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(byte id) {
                  Brands brands = db.Brands.Find(id);
                  brands.IsActive = false;
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
