using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelloWorld.Models.ViewModels
{
    public class ServiceVersionVM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceVersionVM()
        {
            this.Operations = new HashSet<Operation>();
            this.ServiceLogs = new HashSet<ServiceLog>();
            this.Workflows = new HashSet<Workflow>();
        }

        public int ServiceVersionId { get; set; }
        public Nullable<int> ServiceId { get; set; }
        public string Version { get; set; }
        [Display(Name = "Endpoint Url")]
        public string EndpointUrl { get; set; }
        public string WSDL { get; set; }
        public string Availability { get; set; }
        [Display(Name = "Response Time")]
        public string ResponseTime { get; set; }
        [Display(Name = "Adaptor URL")]
        public string AdaptorURL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Operation> Operations { get; set; }
        public virtual Service Service { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceLog> ServiceLogs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Workflow> Workflows { get; set; }
    }
}