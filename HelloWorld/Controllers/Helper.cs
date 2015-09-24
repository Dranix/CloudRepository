using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWorld.Controllers
{
    public class Helper
    {
        public static string SetSortViewBag(string sortOrder, string asc, string desc)
        {
            if (String.IsNullOrEmpty(sortOrder))
            {
                return asc;
            }
            else
            {
                if (sortOrder.Equals(asc))
                {
                    return desc;
                }
                else
                {
                    return asc;
                }
            }
        }
    }
}