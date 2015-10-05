using System;
using System.Collections.Generic;

namespace HelloWorld.Models.ViewModels
{
    public class HomeVM
    {
        //Count service
        public int ServiceCount;

        //Newest service top 5
        public List<ServiceVM> ServiceList;

        //Count catogory
        public int CategoryCount;

        //Categories Top 5
        public List<ServiceCategoryVM> CategoryList;

        //Count application
        public int ApplicationCount;

        //Application top 5 most use app
        public List<EnterpriseApplicationVM> ApplicationList;
    }
}