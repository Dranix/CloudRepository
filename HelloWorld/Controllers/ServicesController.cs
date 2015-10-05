using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HelloWorld.Models;
using HelloWorld.Models.ViewModels;
using PagedList;
using System;
using System.Web;
using System.IO;
using System.Drawing;

namespace HelloWorld.Controllers
{
    public class ServicesController : Controller
    {
        private Entities db = new Entities();

        // GET: EnterpriseApplications
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.ServiceName = Helper.SetSortViewBag(sortOrder, "ServiceName", "ServiceName_Desc");
            ViewBag.ServiceDescription = Helper.SetSortViewBag(sortOrder, "ServiceDescription", "ServiceDescription_Desc");
            ViewBag.SortSLA = Helper.SetSortViewBag(sortOrder, "SLA", "SLA_Desc");
            ViewBag.SortSupportUrl = Helper.SetSortViewBag(sortOrder, "SupportUrl", "SupportUrl_Desc");
            ViewBag.SortServiceCost = Helper.SetSortViewBag(sortOrder, "ServiceCost", "ServiceCost_Desc");
            ViewBag.SortServiceSecurity = Helper.SetSortViewBag(sortOrder, "ServiceSecurity", "ServiceSecurity_Desc");
            ViewBag.SortServiceStatus = Helper.SetSortViewBag(sortOrder, "ServiceStatus", "ServiceStatus_Desc");
            ViewBag.SortServiceType = Helper.SetSortViewBag(sortOrder, "ServiceType", "ServiceType_Desc");
            ViewBag.SortCategoryName = Helper.SetSortViewBag(sortOrder, "CategoryName", "CategoryName_Desc");
            ViewBag.SortProviderName = Helper.SetSortViewBag(sortOrder, "ProviderName", "ProviderName_Desc");

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

            var vm = AutoMapper.Mapper.Map<List<ServiceVM>>(db.Services.ToList());
            if (!String.IsNullOrEmpty(searchString))
            {
                vm = vm.Where(s => s.ServiceName != null && s.ServiceName.ToLower().Contains(searchString)
                                       || s.ServiceDescription != null && s.ServiceDescription.ToLower().Contains(searchString)
                                       || s.SLA != null && s.SLA.ToLower().Contains(searchString)
                                       || s.SupportUrl != null && s.SupportUrl.ToLower().Contains(searchString)
                                       || s.ServiceCost != null && s.ServiceCost.ToLower().Contains(searchString)
                                       || s.ServiceSecurity != null && s.ServiceSecurity.ToLower().Contains(searchString)
                                       || s.ServiceStatus != null && s.ServiceStatus.ToLower().Contains(searchString)
                                       || s.ServiceType != null && s.ServiceType.ToLower().Contains(searchString)
                                       || s.ServiceCategory != null && s.ServiceCategory.CategoryName.ToLower().Contains(searchString)
                                       || s.ServiceProvider != null && s.ServiceProvider.ProviderName.ToLower().Contains(searchString)
                                       ).ToList();
            }

            switch (sortOrder)
            {
                case "ServiceName":
                    vm = vm.OrderBy(m => m.ServiceName).ToList();
                    break;

                case "ServiceName_Desc":
                    vm = vm.OrderByDescending(m => m.ServiceName).ToList();
                    break;

                case "ServiceDescription":
                    vm = vm.OrderBy(m => m.ServiceDescription).ToList();
                    break;

                case "ServiceDescription_Desc":
                    vm = vm.OrderByDescending(m => m.ServiceDescription).ToList();
                    break;

                case "SLA":
                    vm = vm.OrderBy(m => m.SLA).ToList();
                    break;

                case "SLA_Desc":
                    vm = vm.OrderByDescending(m => m.SLA).ToList();
                    break;

                case "SupportUrl":
                    vm = vm.OrderBy(m => m.SupportUrl).ToList();
                    break;

                case "SupportUrl_Desc":
                    vm = vm.OrderByDescending(m => m.SupportUrl).ToList();
                    break;

                case "ServiceCost":
                    vm = vm.OrderBy(m => m.ServiceCost).ToList();
                    break;

                case "ServiceCost_Desc":
                    vm = vm.OrderByDescending(m => m.ServiceCost).ToList();
                    break;

                case "ServiceSecurity":
                    vm = vm.OrderBy(m => m.ServiceSecurity).ToList();
                    break;

                case "ServiceSecurity_Desc":
                    vm = vm.OrderByDescending(m => m.ServiceSecurity).ToList();
                    break;

                case "ServiceStatus":
                    vm = vm.OrderBy(m => m.ServiceStatus).ToList();
                    break;

                case "ServiceStatus_Desc":
                    vm = vm.OrderByDescending(m => m.ServiceStatus).ToList();
                    break;

                case "ServiceType":
                    vm = vm.OrderBy(m => m.ServiceType).ToList();
                    break;

                case "ServiceType_Desc":
                    vm = vm.OrderByDescending(m => m.ServiceType).ToList();
                    break;

                case "CategoryName":
                    vm = vm.OrderBy(m => m.ServiceCategory != null ? String.Empty : m.ServiceCategory.CategoryName).ToList();
                    break;

                case "CategoryName_Desc":
                    vm = vm.OrderByDescending(m => m.ServiceCategory != null ? String.Empty : m.ServiceCategory.CategoryName).ToList();
                    break;

                case "ProviderName":
                    vm = vm.OrderBy(m => m.ServiceProvider != null ? String.Empty : m.ServiceProvider.ProviderName).ToList();
                    break;

                case "ProviderName_Desc":
                    vm = vm.OrderByDescending(m => m.ServiceProvider != null ? String.Empty : m.ServiceProvider.ProviderName).ToList();
                    break;

                default:
                    vm = vm.OrderBy(m => m.ServiceId).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(vm.ToPagedList(pageNumber, pageSize));
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }

            var vm = AutoMapper.Mapper.Map<ServiceVM>(service);
            return View(vm);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "ServiceName");
            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "ServiceName");
            ViewBag.ServiceCategoryId = new SelectList(db.ServiceCategories, "ServiceCategoryId", "CategoryName");
            ViewBag.ServiceProviderId = new SelectList(db.ServiceProviders, "ServiceProviderId", "ProviderName");
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceId,ServiceProviderId,ServiceCategoryId,ServiceName,ServiceDescription,SLA,SupportUrl,ServiceCost,ServiceSecurity,ServiceStatus,ServiceType")] ServiceVM serviceVM, HttpPostedFileBase uploadImage)
        {
            var service = AutoMapper.Mapper.Map<Service>(serviceVM);

            if (ModelState.IsValid)
            {
                byte[] imageData = null;

                if (uploadImage.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                        Image image = (Bitmap)((new ImageConverter()).ConvertFrom(imageData));
                        Image resizeImage = Helper.ResizeImage(image, 480, 480);
                        imageData = (byte[])((new ImageConverter()).ConvertTo(resizeImage, typeof(byte[])));
                    }
                }

                service.ServiceLogo = imageData;
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "ServiceName", service.ServiceId);
            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "ServiceName", service.ServiceId);
            ViewBag.ServiceCategoryId = new SelectList(db.ServiceCategories, "ServiceCategoryId", "CategoryName", service.ServiceCategoryId);
            ViewBag.ServiceProviderId = new SelectList(db.ServiceProviders, "ServiceProviderId", "ProviderName", service.ServiceProviderId);

            return View(serviceVM);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ViewImage(int id)
        {
            var item = db.Services.Where(x => x.ServiceId == id).First();
            byte[] buffer = item.ServiceLogo;
            return File(buffer, "image/jpg", string.Format("{0}.jpg", id));
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "ServiceName", service.ServiceId);
            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "ServiceName", service.ServiceId);
            ViewBag.ServiceCategoryId = new SelectList(db.ServiceCategories, "ServiceCategoryId", "CategoryName", service.ServiceCategoryId);
            ViewBag.ServiceProviderId = new SelectList(db.ServiceProviders, "ServiceProviderId", "ProviderName", service.ServiceProviderId);

            var vm = AutoMapper.Mapper.Map<ServiceVM>(service);
            return View(vm);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceId,ServiceProviderId,ServiceCategoryId,ServiceName,ServiceDescription,SLA,SupportUrl,ServiceCost,ServiceSecurity,ServiceStatus,ServiceType")] Service service, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;

                if (uploadImage.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                        Image image = (Bitmap)((new ImageConverter()).ConvertFrom(imageData));
                        Image resizeImage = Helper.ResizeImage(image, 480, 480);
                        imageData = (byte[])((new ImageConverter()).ConvertTo(resizeImage, typeof(byte[])));
                    }
                }

                service.ServiceLogo = imageData;
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "ServiceName", service.ServiceId);
            ViewBag.ServiceId = new SelectList(db.Services, "ServiceId", "ServiceName", service.ServiceId);
            ViewBag.ServiceCategoryId = new SelectList(db.ServiceCategories, "ServiceCategoryId", "CategoryName", service.ServiceCategoryId);
            ViewBag.ServiceProviderId = new SelectList(db.ServiceProviders, "ServiceProviderId", "ProviderName", service.ServiceProviderId);

            var vm = AutoMapper.Mapper.Map<ServiceVM>(service);
            return View(vm);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }

            var vm = AutoMapper.Mapper.Map<ServiceVM>(service);
            return View(vm);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
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
