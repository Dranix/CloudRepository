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
    public class WorkflowsController : Controller
    {
        private Entities db = new Entities();

        // GET: Workflows
        public ActionResult Index()
        {
            var workflows = db.Workflows.Include(w => w.EnterpriseApplication).Include(w => w.ServiceVersion);
            return View(workflows.ToList());
        }

        // GET: Workflows/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workflow workflow = db.Workflows.Find(id);
            if (workflow == null)
            {
                return HttpNotFound();
            }
            return View(workflow);
        }

        // GET: Workflows/Create
        public ActionResult Create()
        {
            ViewBag.EnterpriseApplicationId = new SelectList(db.EnterpriseApplications, "EnterpriseApplicationId", "ApplicationName");
            ViewBag.ServiceVersionId = new SelectList(db.ServiceVersions, "ServiceVersionId", "Version");
            return View();
        }

        // POST: Workflows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkflowId,ServiceVersionId,EnterpriseApplicationId")] Workflow workflow)
        {
            if (ModelState.IsValid)
            {
                db.Workflows.Add(workflow);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EnterpriseApplicationId = new SelectList(db.EnterpriseApplications, "EnterpriseApplicationId", "ApplicationName", workflow.EnterpriseApplicationId);
            ViewBag.ServiceVersionId = new SelectList(db.ServiceVersions, "ServiceVersionId", "Version", workflow.ServiceVersionId);
            return View(workflow);
        }

        // GET: Workflows/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workflow workflow = db.Workflows.Find(id);
            if (workflow == null)
            {
                return HttpNotFound();
            }
            ViewBag.EnterpriseApplicationId = new SelectList(db.EnterpriseApplications, "EnterpriseApplicationId", "ApplicationName", workflow.EnterpriseApplicationId);
            ViewBag.ServiceVersionId = new SelectList(db.ServiceVersions, "ServiceVersionId", "Version", workflow.ServiceVersionId);
            return View(workflow);
        }

        // POST: Workflows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkflowId,ServiceVersionId,EnterpriseApplicationId")] Workflow workflow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workflow).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EnterpriseApplicationId = new SelectList(db.EnterpriseApplications, "EnterpriseApplicationId", "ApplicationName", workflow.EnterpriseApplicationId);
            ViewBag.ServiceVersionId = new SelectList(db.ServiceVersions, "ServiceVersionId", "Version", workflow.ServiceVersionId);
            return View(workflow);
        }

        // GET: Workflows/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workflow workflow = db.Workflows.Find(id);
            if (workflow == null)
            {
                return HttpNotFound();
            }
            return View(workflow);
        }

        // POST: Workflows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Workflow workflow = db.Workflows.Find(id);
            db.Workflows.Remove(workflow);
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
