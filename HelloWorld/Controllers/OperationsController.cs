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
    public class OperationsController : Controller
    {
        private Entities db = new Entities();

        // GET: EnterpriseApplications
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.SortOperationName = Helper.SetSortViewBag(sortOrder, "OperationName", "OperationName_Desc");
            ViewBag.SortServiceParameter = Helper.SetSortViewBag(sortOrder, "ServiceParameter", "ServiceParameter_Desc");
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

            var vm = AutoMapper.Mapper.Map<List<OperationVM>>(db.Operations.ToList());
            if (!String.IsNullOrEmpty(searchString))
            {
                vm = vm.Where(s => s.OperationName != null && s.OperationName.ToLower().Contains(searchString)
                                       //|| s.ServiceParameter != null && s.ServiceParameter.ToLower().Contains(searchString)
                                       ).ToList();
            }

            switch (sortOrder)
            {
                case "OperationName":
                    vm = vm.OrderBy(m => m.OperationName).ToList();
                    break;

                case "OperationName_Desc":
                    vm = vm.OrderByDescending(m => m.OperationName).ToList();
                    break;

                case "ServiceParameter":
                    //vm = vm.OrderBy(m => m.ServiceParameter).ToList();
                    break;

                case "ServiceParameter_Desc":
                    //vm = vm.OrderByDescending(m => m.ServiceParameter).ToList();
                    break;

                default:
                    vm = vm.OrderBy(m => m.OperationId).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(vm.ToPagedList(pageNumber, pageSize));
        }

        // GET: Operations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operation operation = db.Operations.Find(id);
            if (operation == null)
            {
                return HttpNotFound();
            }
            var vm = AutoMapper.Mapper.Map<OperationVM>(operation);
            return View(vm);
        }

        // GET: Operations/Create
        public ActionResult Create()
        {
            var vm = AutoMapper.Mapper.Map<List<ServiceVersionVM>>(db.ServiceVersions.ToList());
            ViewBag.ServiceVersionId = new SelectList(vm, "ServiceVersionId", "Version");
            return View();
        }

        // POST: Operations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OperationId,ServiceVersionId,OperationName,ServiceParameter")] OperationVM operationVM)
        {
            var operation = AutoMapper.Mapper.Map<Operation>(operationVM);

            if (ModelState.IsValid)
            {
                db.Operations.Add(operation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ServiceVersionId = new SelectList(db.ServiceVersions, "ServiceVersionId", "Version", operation.ServiceVersionId);

            return View(operationVM);
        }

        // GET: Operations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operation operation = db.Operations.Find(id);
            if (operation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceVersionId = new SelectList(db.ServiceVersions, "ServiceVersionId", "Version", operation.ServiceVersionId);

            var vm = AutoMapper.Mapper.Map<OperationVM>(operation);
            return View(vm);
        }

        // POST: Operations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OperationId,ServiceVersionId,OperationName,ServiceParameter")] Operation operation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(operation).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceVersionId = new SelectList(db.ServiceVersions, "ServiceVersionId", "Version", operation.ServiceVersionId);

            var vm = AutoMapper.Mapper.Map<OperationVM>(operation);
            return View(vm);
        }

        // GET: Operations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operation operation = db.Operations.Find(id);
            if (operation == null)
            {
                return HttpNotFound();
            }

            var vm = AutoMapper.Mapper.Map<OperationVM>(operation);
            return View(vm);
        }

        // POST: Operations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Operation operation = db.Operations.Find(id);
            db.Operations.Remove(operation);
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
