using HelloWorld.Models;
using HelloWorld.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace HelloWorld.Controllers
{
    public class OverviewController : Controller
    {
        private Entities db = new Entities();

        // GET: Overview
        public ActionResult Index()
        {
            OverviewVM vm = new OverviewVM();
            vm.Categories= AutoMapper.Mapper.Map<List<ServiceCategoryVM>>(db.ServiceCategories.ToList());

            return View(vm);
        }


        public ActionResult ListService(int? categoryId, string searchString)
        {
            if (categoryId == null)
            {
                categoryId = (int)Session["categiryId"];
            }
            else
            {
                Session["categiryId"] = categoryId;
            }

            if (String.IsNullOrEmpty(searchString))
            {
                searchString = Session["searchString"] != null ? Session["searchString"].ToString() : null;
            }
            else
            {
                Session["searchString"] = searchString;
            }

            var vm = AutoMapper.Mapper.Map<List<ServiceVM>>(db.Services.Where(s=>s.ServiceCategory.ServiceCategoryId == categoryId).ToList());
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

            return PartialView(vm);
        }





        public ActionResult SelectCategory(string sortOrder, string searchString, string currentFilter, int? page, int? CategoryId)
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
            return PartialView(vm.ToPagedList(pageNumber, pageSize));
        }
    }
}