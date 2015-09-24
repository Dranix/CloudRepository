using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWorld.Models.ViewModels
{
    public class ParameterVM
    {
        public int ParameterId { get; set; }
        public string ParameterName { get; set; }
        public string ParameterType { get; set; }
        public string ParameterDescription { get; set; }
        public int OperationId { get; set; }

        public virtual Operation Operation { get; set; }
    }
}