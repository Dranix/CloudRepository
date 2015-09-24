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
    public class ServiceProvidersController : Controller
    {
        private Entities db = new Entities();

        // GET: EnterpriseApplications
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.SortProviderName = Helper.SetSortViewBag(sortOrder, "ProviderName", "ProviderName_Desc");
            ViewBag.SortWebsite = Helper.SetSortViewBag(sortOrder, "Website", "Website_Desc");
            ViewBag.SortSupportEmail = Helper.SetSortViewBag(sortOrder, "SupportEmail", "SupportEmail_Desc");
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

            var vm = AutoMapper.Mapper.Map<List<ServiceProviderVM>>(db.ServiceProviders.ToList());
            if (!String.IsNullOrEmpty(searchString))
            {
                vm = vm.Where(s => s.ProviderName != null && s.ProviderName.ToLower().Contains(searchString)
                                       || s.Website != null && s.Website.ToLower().Contains(searchString)
                                       || s.SupportEmail != null && s.SupportEmail.ToLower().Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "ProviderName":
                    vm = vm.OrderBy(m => m.ProviderName).ToList();
                    break;

                case "ProviderName_Desc":
                    vm = vm.OrderByDescending(m => m.ProviderName).ToList();
                    break;

                case "Website":
                    vm = vm.OrderBy(m => m.Website).ToList();
                    break;

                case "Website_Desc":
                    vm = vm.OrderByDescending(m => m.Website).ToList();
                    break;

                case "SupportEmail":
                    vm = vm.OrderBy(m => m.SupportEmail).ToList();
                    break;

                case "SupportEmail_Desc":
                    vm = vm.OrderByDescending(m => m.SupportEmail).ToList();
                    break;

                default:
                    vm = vm.OrderBy(m => m.ServiceProviderId).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(vm.ToPagedList(pageNumber, pageSize));
        }

        // GET: ServiceProviders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceProvider serviceProvider = db.ServiceProviders.Find(id);
            if (serviceProvider == null)
            {
                return HttpNotFound();
            }

            var vm = AutoMapper.Mapper.Map<ServiceProviderVM>(serviceProvider);
            return View(vm);
        }

        // GET: ServiceProviders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceProviders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceProviderId,ProviderName,Website,Phone,SupportEmail")] ServiceProvider serviceProvider)
        {
            if (ModelState.IsValid)
            {
                var model = AutoMapper.Mapper.Map<ServiceProvider>(serviceProvider);
                db.ServiceProviders.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serviceProvider);
        }

        // GET: ServiceProviders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceProvider serviceProvider = db.ServiceProviders.Find(id);
            if (serviceProvider == null)
            {
                return HttpNotFound();
            }

            var vm = AutoMapper.Mapper.Map<ServiceProviderVM>(serviceProvider);
            return View(vm);
        }

        // POST: ServiceProviders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceProviderId,ProviderName,Website,Phone,SupportEmail")] ServiceProvider serviceProvider)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceProvider).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var vm = AutoMapper.Mapper.Map<ServiceProviderVM>(serviceProvider);
            return View(vm);
        }

        // GET: ServiceProviders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceProvider serviceProvider = db.ServiceProviders.Find(id);
            if (serviceProvider == null)
            {
                return HttpNotFound();
            }

            var vm = AutoMapper.Mapper.Map<ServiceProviderVM>(serviceProvider);
            return View(vm);
        }

        // POST: ServiceProviders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceProvider serviceProvider = db.ServiceProviders.Find(id);
            db.ServiceProviders.Remove(serviceProvider);
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
