using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelloWorld.Models;

namespace HelloWorld.Controllers
{
    public class ServiceVersions1Controller : Controller
    {
        private Entities db = new Entities();

        // GET: ServiceVersions1
        public ActionResult Index()
        {
            var serviceVersions = db.ServiceVersions;
            return View(serviceVersions.ToList());
        }

        // GET: ServiceVersions1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceVersion serviceVersion = db.ServiceVersions.Find(id);
            if (serviceVersion == null)
            {
                return HttpNotFound();
            }
            return View(serviceVersion);
        }

        // GET: ServiceVersions1/Create
        public ActionResult Create()
        {
            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "ServiceName");
            return View();
        }

        // POST: ServiceVersions1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceVersionId,ServiceId,Version,EndpointUrl,WSDL,Availability,ResponseTime,AdaptorURL")] ServiceVersion serviceVersion)
        {
            if (ModelState.IsValid)
            {
                db.ServiceVersions.Add(serviceVersion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "ServiceName", serviceVersion.ServiceId);
            return View(serviceVersion);
        }

        // GET: ServiceVersions1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceVersion serviceVersion = db.ServiceVersions.Find(id);
            if (serviceVersion == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "ServiceName", serviceVersion.ServiceId);
            return View(serviceVersion);
        }

        // POST: ServiceVersions1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceVersionId,ServiceId,Version,EndpointUrl,WSDL,Availability,ResponseTime,AdaptorURL")] ServiceVersion serviceVersion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceVersion).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "ServiceName", serviceVersion.ServiceId);
            return View(serviceVersion);
        }

        // GET: ServiceVersions1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceVersion serviceVersion = db.ServiceVersions.Find(id);
            if (serviceVersion == null)
            {
                return HttpNotFound();
            }
            return View(serviceVersion);
        }

        // POST: ServiceVersions1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceVersion serviceVersion = db.ServiceVersions.Find(id);
            db.ServiceVersions.Remove(serviceVersion);
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
