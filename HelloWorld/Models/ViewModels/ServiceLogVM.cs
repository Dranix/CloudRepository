using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloWorld.Models.ViewModels
{
    public class ServiceLogVM
    {
        public int ServiceLogId { get; set; }
        public Nullable<int> ServiceVersionId { get; set; }
        [Display(Name = "Execution Start Time")]
        public string ExecutionStartTime { get; set; }
        [Display(Name = "Execution End Time")]
        public string ExecutionEndTime { get; set; }
        [Display(Name = "Log Message")]
        public string LogMessage { get; set; }
        [Display(Name = "Audit Status")]
        public string AuditStatus { get; set; }

        public virtual ServiceVersion ServiceVersion { get; set; }
    }
}