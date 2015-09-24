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
    public class EnterpriseApplicationsController : Controller
    {
        private Entities db = new Entities();

        // GET: EnterpriseApplications
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.SortApplicationName = Helper.SetSortViewBag(sortOrder, "ApplicationName", "ApplicationName_Desc");
            ViewBag.SortSpecifications = Helper.SetSortViewBag(sortOrder, "Specifications", "Specifications_Desc");
            ViewBag.SortUsingServices = Helper.SetSortViewBag(sortOrder, "UsingServices", "UsingServices_Desc");
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

            var vm = AutoMapper.Mapper.Map<List<EnterpriseApplicationVM>>(db.EnterpriseApplications.ToList());
            if (!String.IsNullOrEmpty(searchString))
            {
                vm = vm.Where(s => s.ApplicationName != null && s.ApplicationName.ToLower().Contains(searchString)
                                       || s.Specifications != null && s.Specifications.ToLower().Contains(searchString)).ToList();
                
                                //       || s.UsingServices != null && s.UsingServices.ToLower().Contains(searchString)
            }

            switch (sortOrder)
            {
                case "ApplicationName":
                    vm = vm.OrderBy(m => m.ApplicationName).ToList();
                    break;

                case "ApplicationName_Desc":
                    vm = vm.OrderByDescending(m => m.ApplicationName).ToList();
                    break;

                case "Specifications":
                    vm = vm.OrderBy(m => m.Specifications).ToList();
                    break;

                case "Specifications_Desc":
                    vm = vm.OrderByDescending(m => m.Specifications).ToList();
                    break;

                case "UsingServices":
                    vm = vm.OrderBy(m => m.UsingServices).ToList();
                    break;

                case "UsingServices_Desc":
                    vm = vm.OrderByDescending(m => m.UsingServices).ToList();
                    break;

                default:
                    vm = vm.OrderBy(m => m.EnterpriseApplicationId).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(vm.ToPagedList(pageNumber, pageSize));
        }

        // GET: EnterpriseApplications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnterpriseApplication enterpriseApplication = db.EnterpriseApplications.Find(id);
            if (enterpriseApplication == null)
            {
                return HttpNotFound();
            }
            var vm = AutoMapper.Mapper.Map<EnterpriseApplicationVM>(enterpriseApplication);
            return View(vm);
        }

        // GET: EnterpriseApplications/Create
        public ActionResult Create()
        {
            var serviceVersionList = db.ServiceVersions.Select(sv => new
            {
                ServiceVersionId = sv.ServiceVersionId,
                ServiceName = sv.Service.ServiceName + " " + sv.Version
            }).ToList();

            ViewBag.ServiceVersionList = new MultiSelectList(serviceVersionList, "ServiceVersionId", "ServiceName");

            return View();
        }

        // POST: EnterpriseApplications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnterpriseApplicationId,ApplicationName,Specifications,UsingServices")] EnterpriseApplicationVM enterpriseApplicationVM, int[] serviceVersionList)
        {

            var enterpriseApplication = AutoMapper.Mapper.Map<EnterpriseApplication>(enterpriseApplicationVM);
            if (ModelState.IsValid)
            {
                db.EnterpriseApplications.Add(enterpriseApplication);
                int insertedRecord = db.SaveChanges();

                if (insertedRecord > 0)
                {
                    if (serviceVersionList != null && serviceVersionList .Count()> 0)
                    {
                        Workflow workFlow = new Workflow();
                        foreach (int serviceVersionId in serviceVersionList)
                        {
                            workFlow.EnterpriseApplicationId = enterpriseApplication.EnterpriseApplicationId;
                            workFlow.ServiceVersionId = serviceVersionId;
                            db.Workflows.Add(workFlow);

                            db.SaveChanges();
                        }
                    }
                }
                return RedirectToAction("Index");
            }

            var vm = AutoMapper.Mapper.Map<EnterpriseApplicationVM>(enterpriseApplication);
            return View(vm);
        }

        // GET: EnterpriseApplications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnterpriseApplication enterpriseApplication = db.EnterpriseApplications.Find(id);
            if (enterpriseApplication == null)
            {
                return HttpNotFound();
            }
            var vm = AutoMapper.Mapper.Map<EnterpriseApplicationVM>(enterpriseApplication);
            return View(vm);
        }

        // POST: EnterpriseApplications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnterpriseApplicationId,ApplicationName,Specifications,UsingServices")] EnterpriseApplicationVM enterpriseApplicationVM)
        {
            var enterpriseApplication = AutoMapper.Mapper.Map<EnterpriseApplication>(enterpriseApplicationVM);

            if (ModelState.IsValid)
            {
                db.Entry(enterpriseApplication).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(enterpriseApplication);
        }

        // GET: EnterpriseApplications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnterpriseApplication enterpriseApplication = db.EnterpriseApplications.Find(id);
            if (enterpriseApplication == null)
            {
                return HttpNotFound();
            }
            var vm = AutoMapper.Mapper.Map<EnterpriseApplicationVM>(enterpriseApplication);
            return View(vm);
        }

        // POST: EnterpriseApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EnterpriseApplication enterpriseApplication = db.EnterpriseApplications.Find(id);
            db.EnterpriseApplications.Remove(enterpriseApplication);
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
