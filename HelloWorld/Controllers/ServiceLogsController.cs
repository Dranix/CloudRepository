using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelloWorld.Models;
using HelloWorld.Models.ViewModels;
using PagedList;

namespace HelloWorld.Controllers
{
    public class ServiceLogsController : Controller
    {
        private Entities db = new Entities();

        // GET: EnterpriseApplications
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            //Execution Start Time	Execution End Time	Log Message	Audit Status	Version
            ViewBag.SortExecutionStartTime = Helper.SetSortViewBag(sortOrder, "ExecutionStartTime", "ExecutionStartTime_Desc");
            ViewBag.SortExecutionEndTime = Helper.SetSortViewBag(sortOrder, "ExecutionEndTime", "ExecutionEndTime_Desc");
            ViewBag.SortLogMessage = Helper.SetSortViewBag(sortOrder, "LogMessage", "LogMessage_Desc");
            ViewBag.SortAudit = Helper.SetSortViewBag(sortOrder, "LogMessage", "LogMessage_Desc");
            ViewBag.SortStatus = Helper.SetSortViewBag(sortOrder, "Status", "Status_Desc");
            ViewBag.SortVersion = Helper.SetSortViewBag(sortOrder, "Version", "Version_Desc");

            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var vm = AutoMapper.Mapper.Map<List<ServiceLogVM>>(db.ServiceLogs.ToList());
            if (!String.IsNullOrEmpty(searchString))
            {
                vm = vm.Where(s => s.ExecutionStartTime != null && s.ExecutionStartTime.ToLower().Contains(searchString)
                                       || s.ExecutionEndTime != null && s.ExecutionEndTime.ToLower().Contains(searchString)
                                       || s.LogMessage != null && s.LogMessage.ToLower().Contains(searchString)
                                       || s.AuditStatus != null && s.AuditStatus.ToLower().Contains(searchString)
                                       || s.AuditStatus != null && s.AuditStatus.ToLower().Contains(searchString)
                                       || s.ServiceVersion != null && s.ServiceVersion.Version.ToLower().Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "ExecutionStartTime":
                    vm = vm.OrderBy(m => m.ExecutionStartTime).ToList();
                    break;

                case "ExecutionStartTime_Desc":
                    vm = vm.OrderByDescending(m => m.ExecutionStartTime).ToList();
                    break;

                case "ExecutionEndTime":
                    vm = vm.OrderBy(m => m.ExecutionEndTime).ToList();
                    break;

                case "ExecutionEndTime_Desc":
                    vm = vm.OrderByDescending(m => m.ExecutionEndTime).ToList();
                    break;

                case "LogMessage":
                    vm = vm.OrderBy(m => m.LogMessage).ToList();
                    break;

                case "LogMessage_Desc":
                    vm = vm.OrderByDescending(m => m.LogMessage).ToList();
                    break;

                case "Status":
                    vm = vm.OrderBy(m => m.AuditStatus).ToList();
                    break;

                case "Status_Desc":
                    vm = vm.OrderByDescending(m => m.AuditStatus).ToList();
                    break;

                case "Version":
                    vm = vm.OrderBy(m => m.ServiceVersion == null ? String.Empty : m.ServiceVersion.Version).ToList();
                    break;

                case "Version_Desc":
                    vm = vm.OrderByDescending(m => m.ServiceVersion == null ? String.Empty : m.ServiceVersion.Version).ToList();
                    break;

                default:
                    vm = vm.OrderBy(m => m.ServiceLogId).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(vm.ToPagedList(pageNumber, pageSize));
        }

        // GET: ServiceLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceLog serviceLog = db.ServiceLogs.Find(id);
            if (serviceLog == null)
            {
                return HttpNotFound();
            }
            var vm = AutoMapper.Mapper.Map<ServiceLogVM>(serviceLog);
            return View(vm);
        }

        // GET: ServiceLogs/Create
        public ActionResult Create()
        {
            ViewBag.ServiceVersionId = new SelectList(db.ServiceVersions, "ServiceVersionId", "Version");
            return View();
        }

        // POST: ServiceLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceLogId,ServiceVersionId,ExecutionStartTime,ExecutionEndTime,LogMessage,AuditStatus")] ServiceLogVM serviceLogVM)
        {
            var serviceLog = AutoMapper.Mapper.Map<ServiceLog>(serviceLogVM);

            if (ModelState.IsValid)
            {
                db.ServiceLogs.Add(serviceLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ServiceVersionId = new SelectList(db.ServiceVersions, "ServiceVersionId", "Version", serviceLog.ServiceVersionId);
            return View(serviceLog);
        }

        // GET: ServiceLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceLog serviceLog = db.ServiceLogs.Find(id);
            if (serviceLog == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceVersionId = new SelectList(db.ServiceVersions, "ServiceVersionId", "Version", serviceLog.ServiceVersionId);
            var vm = AutoMapper.Mapper.Map<ServiceLogVM>(serviceLog);
            return View(vm);
        }

        // POST: ServiceLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceLogId,ServiceVersionId,ExecutionStartTime,ExecutionEndTime,LogMessage,AuditStatus")] ServiceLogVM serviceLogVM)
        {
            var serviceLog = AutoMapper.Mapper.Map<ServiceLog>(serviceLogVM);

            if (ModelState.IsValid)
            {
                db.Entry(serviceLog).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceVersionId = new SelectList(db.ServiceVersions, "ServiceVersionId", "Version", serviceLog.ServiceVersionId);
            return View(serviceLog);
        }

        // GET: ServiceLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceLog serviceLog = db.ServiceLogs.Find(id);
            if (serviceLog == null)
            {
                return HttpNotFound();
            }
            var vm = AutoMapper.Mapper.Map<ServiceLogVM>(serviceLog);
            return View(vm);
        }

        // POST: ServiceLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceLog serviceLog = db.ServiceLogs.Find(id);
            db.ServiceLogs.Remove(serviceLog);
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
