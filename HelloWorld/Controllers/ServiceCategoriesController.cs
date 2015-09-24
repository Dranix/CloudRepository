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
    public class ServiceCategoriesController : Controller
    {
        private Entities db = new Entities();

        // GET: EnterpriseApplications
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.SortParentCategory = Helper.SetSortViewBag(sortOrder, "ParentCategory", "ParentCategory_Desc");
            ViewBag.SortCategoryName = Helper.SetSortViewBag(sortOrder, "CategoryName", "CategoryName_Desc");
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

            var vm = AutoMapper.Mapper.Map<List<ServiceCategoryVM>>(db.ServiceCategories.ToList());
            if (!String.IsNullOrEmpty(searchString))
            {
                vm = vm.Where(s => s.CategoryName != null && s.CategoryName.ToLower().Contains(searchString)
                                       || s.ParentCategory != null && s.ParentCategory.CategoryName.ToLower().Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "ParentCategory":
                    vm = vm.OrderBy(m => m.ParentCategory == null ? String.Empty : m.ParentCategory.CategoryName).ToList();
                    break;

                case "ParentCategory_Desc":
                    vm = vm.OrderByDescending(m => m.ParentCategory == null ? String.Empty : m.ParentCategory.CategoryName).ToList();
                    break;

                case "CategoryName":
                    vm = vm.OrderBy(m => m.CategoryName).ToList();
                    break;

                case "CategoryName_Desc":
                    vm = vm.OrderByDescending(m => m.CategoryName).ToList();
                    break;

                default:
                    vm = vm.OrderBy(m => m.ServiceCategoryId).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(vm.ToPagedList(pageNumber, pageSize));
        }

        // GET: ServiceCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceCategory serviceCategory = db.ServiceCategories.Find(id);
            if (serviceCategory == null)
            {
                return HttpNotFound();
            }

            var vm = AutoMapper.Mapper.Map<ServiceCategoryVM>(serviceCategory);
            return View(vm);
        }

        // GET: ServiceCategories/Create
        public ActionResult Create()
        {
            ViewBag.ServiceCategoryParent = new SelectList(db.ServiceCategories, "ServiceCategoryId", "CategoryName");
            return View();
        }

        // POST: ServiceCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceCategoryId,CategoryName,ServiceCategoryParent")] ServiceCategory serviceCategory)
        {
            if (ModelState.IsValid)
            {
                var model = AutoMapper.Mapper.Map<ServiceCategory>(serviceCategory);
                db.ServiceCategories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ServiceCategoryParent = new SelectList(db.ServiceCategories, "ServiceCategoryId", "CategoryName", serviceCategory.ServiceCategoryParent);
            return View(serviceCategory);
        }

        // GET: ServiceCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceCategory serviceCategory = db.ServiceCategories.Find(id);
            if (serviceCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceCategoryParent = new SelectList(db.ServiceCategories, "ServiceCategoryId", "CategoryName", serviceCategory.ServiceCategoryParent);

            var vm = AutoMapper.Mapper.Map<ServiceCategoryVM>(serviceCategory);
            return View(vm);
        }

        // POST: ServiceCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceCategoryId,CategoryName,ServiceCategoryParent")] ServiceCategory serviceCategory)
        {
            if (ModelState.IsValid)
            {
                var model = AutoMapper.Mapper.Map<ServiceCategory>(serviceCategory);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceCategoryParent = new SelectList(db.ServiceCategories, "ServiceCategoryId", "CategoryName", serviceCategory.ServiceCategoryParent);
            return View(serviceCategory);
        }

        // GET: ServiceCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceCategory serviceCategory = db.ServiceCategories.Find(id);

            if (serviceCategory == null)
            {
                return HttpNotFound();
            }

            var vm = AutoMapper.Mapper.Map<ServiceCategoryVM>(serviceCategory);
            return View(vm);
        }

        // POST: ServiceCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceCategory serviceCategory = db.ServiceCategories.Find(id);
            db.ServiceCategories.Remove(serviceCategory);
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
