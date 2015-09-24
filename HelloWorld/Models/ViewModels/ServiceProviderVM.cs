using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloWorld.Models.ViewModels
{
    public class ServiceProviderVM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceProviderVM()
        {
            this.Services = new HashSet<Service>();
        }

        public int ServiceProviderId { get; set; }
        [Display(Name = "Provider Name")]
        public string ProviderName { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        [Display(Name = "Support Email")]
        public string SupportEmail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Service> Services { get; set; }
    }
}