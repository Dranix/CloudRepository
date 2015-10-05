using HelloWorld.Models;
using HelloWorld.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWorld.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Index()
        {
            HomeVM vm = new HomeVM();

            vm.ApplicationCount = db.EnterpriseApplications.Count();
            vm.CategoryCount = db.ServiceCategories.Count();
            vm.ServiceCount = db.Services.Count();

            vm.ServiceList = AutoMapper.Mapper.Map<List<ServiceVM>>(db.Services.OrderByDescending(x => x.ServiceId).Take(5).ToList());
            vm.CategoryList = AutoMapper.Mapper.Map<List<ServiceCategoryVM>>(db.ServiceCategories.OrderByDescending(x => x.Services.Count()).Take(5).ToList());
            vm.ApplicationList = AutoMapper.Mapper.Map<List<EnterpriseApplicationVM>>(db.EnterpriseApplications.OrderByDescending(x => x.Workflows.Count()).Take(5).ToList());

            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}