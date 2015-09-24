using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelloWorld.Models.ViewModels
{
    public class OperationVM
    {
        public int OperationId { get; set; }
        public int ServiceVersionId { get; set; }
        [Display(Name = "Operation Name")]
        public string OperationName { get; set; }

        public virtual ServiceVersion ServiceVersion { get; set; }
        public virtual ICollection<Parameter> Parameters { get; set; }
    }
}