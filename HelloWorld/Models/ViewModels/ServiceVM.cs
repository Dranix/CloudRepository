using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloWorld.Models.ViewModels
{
    public class ServiceVM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceVM()
        {
            this.ServiceVersions = new HashSet<ServiceVersion>();
            this.Similars = new HashSet<Similar>();
            this.Similars1 = new HashSet<Similar>();
        }

        public int ServiceId { get; set; }
        public int ServiceProviderId { get; set; }
        public int ServiceCategoryId { get; set; }

        [Display(Name = "Service Name")]
        public string ServiceName { get; set; }

        [Display(Name = "Service Description")]
        public string ServiceDescription { get; set; }

        public string SLA { get; set; }
        [Display(Name = "Support Url")]
        public string SupportUrl { get; set; }
        [Display(Name = "Service Cost")]
        public string ServiceCost { get; set; }
        [Display(Name = "Service Security")]
        public string ServiceSecurity { get; set; }
        [Display(Name = "Service Status")]
        public string ServiceStatus { get; set; }
        [Display(Name = "Service Type")]
        public string ServiceType { get; set; }
        public string ServiceLogo { get; set; }

        public virtual ServiceCategory ServiceCategory { get; set; }
        public virtual ServiceProvider ServiceProvider { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceVersion> ServiceVersions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Similar> Similars { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Similar> Similars1 { get; set; }
    }
}