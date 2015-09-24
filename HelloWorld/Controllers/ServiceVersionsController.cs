using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HelloWorld.Models;
using HelloWorld.Models.ViewModels;
using PagedList;

namespace HelloWorld.Controllers
{
    public class ServiceVersionsController : Controller
    {
        private Entities db = new Entities();

        // GET: EnterpriseApplications
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.SortVersion = Helper.SetSortViewBag(sortOrder, "Version", "Version_Desc");
            ViewBag.SortEndpointUrl = Helper.SetSortViewBag(sortOrder, "EndpointUrl", "EndpointUrl_Desc");
            ViewBag.SortWSDL = Helper.SetSortViewBag(sortOrder, "WSDL", "WSDL_Desc");
            ViewBag.SortAvailability = Helper.SetSortViewBag(sortOrder, "Availability", "Availability_Desc");
            ViewBag.SortResponseTime = Helper.SetSortViewBag(sortOrder, "ResponseTime", "ResponseTime_Desc");
            ViewBag.SortAdaptorURL = Helper.SetSortViewBag(sortOrder, "AdaptorURL", "AdaptorURL_Desc");
            ViewBag.SortServiceName = Helper.SetSortViewBag(sortOrder, "ServiceName", "ServiceName_Desc");

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

            var vm = AutoMapper.Mapper.Map<List<ServiceVersionVM>>(db.ServiceVersions.ToList());
            if (!String.IsNullOrEmpty(searchString))
            {
                vm = vm.Where(s => s.Version != null && s.Version.ToLower().Contains(searchString)
                                       || s.EndpointUrl != null && s.EndpointUrl.ToLower().Contains(searchString)
                                       || s.WSDL != null && s.WSDL.ToLower().Contains(searchString)
                                       || s.Availability != null && s.Availability.ToLower().Contains(searchString)
                                       || s.ResponseTime != null && s.ResponseTime.ToLower().Contains(searchString)
                                       || s.AdaptorURL != null && s.AdaptorURL.ToLower().Contains(searchString)
                                       || s.Service != null && s.Service.ServiceName.ToLower().Contains(searchString)
                                       ).ToList();
            }

            switch (sortOrder)
            {
                case "Version":
                    vm = vm.OrderBy(m => m.Version).ToList();
                    break;

                case "Version_Desc":
                    vm = vm.OrderByDescending(m => m.Version).ToList();
                    break;

                case "EndpointUrl":
                    vm = vm.OrderBy(m => m.EndpointUrl).ToList();
                    break;

                case "EndpointUrl_Desc":
                    vm = vm.OrderByDescending(m => m.EndpointUrl).ToList();
                    break;

                    ViewBag.SortAdaptorURL = Helper.SetSortViewBag(sortOrder, "AdaptorURL", "AdaptorURL_Desc");
                    ViewBag.SortServiceName = Helper.SetSortViewBag(sortOrder, "ServiceName", "ServiceName_Desc");
                case "WSDL":
                    vm = vm.OrderBy(m => m.WSDL).ToList();
                    break;

                case "WSDL_Desc":
                    vm = vm.OrderByDescending(m => m.WSDL).ToList();
                    break;

                case "Availability":
                    vm = vm.OrderBy(m => m.Availability).ToList();
                    break;

                case "Availability_Desc":
                    vm = vm.OrderByDescending(m => m.Availability).ToList();
                    break;

                case "ResponseTime":
                    vm = vm.OrderBy(m => m.ResponseTime).ToList();
                    break;

                case "ResponseTime_Desc":
                    vm = vm.OrderByDescending(m => m.ResponseTime).ToList();
                    break;

                case "AdaptorURL":
                    vm = vm.OrderBy(m => m.AdaptorURL).ToList();
                    break;

                case "AdaptorURL_Desc":
                    vm = vm.OrderByDescending(m => m.AdaptorURL).ToList();
                    break;

                case "ServiceName":
                    vm = vm.OrderBy(m => m.Service != null ? String.Empty : m.Service.ServiceName).ToList();
                    break;

                case "ServiceName_Desc":
                    vm = vm.OrderByDescending(m => m.Service != null ? String.Empty : m.Service.ServiceName).ToList();
                    break;

                default:
                    vm = vm.OrderBy(m => m.ServiceVersionId).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(vm.ToPagedList(pageNumber, pageSize));
        }

        // GET: ServiceVersions/Details/5
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

            var vm = AutoMapper.Mapper.Map<ServiceVersionVM>(serviceVersion);
            return View(vm);
        }

        // GET: ServiceVersions/Create
        public ActionResult Create()
        {
            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "ServiceName");
            return View();
        }

        // POST: ServiceVersions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceVersionId,ServiceId,Version,EndpointUrl,WSDL,Availability,ResponseTime,AdaptorURL")] ServiceVersionVM serviceVersionVM)
        {
            var serviceVersion = AutoMapper.Mapper.Map<ServiceVersion>(serviceVersionVM);
            if (ModelState.IsValid)
            {
                db.ServiceVersions.Add(serviceVersion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "ServiceName", serviceVersion.ServiceId);
            return View(serviceVersion);
        }

        // GET: ServiceVersions/Edit/5
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

            var vm = AutoMapper.Mapper.Map<ServiceVersionVM>(serviceVersion);
            return View(vm);
        }

        // POST: ServiceVersions/Edit/5
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
            var vm = AutoMapper.Mapper.Map<ServiceVersionVM>(serviceVersion);
            return View(vm);
        }

        // GET: ServiceVersions/Delete/5
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
            var vm = AutoMapper.Mapper.Map<ServiceVersionVM>(serviceVersion);
            return View(vm);
        }

        // POST: ServiceVersions/Delete/5
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
