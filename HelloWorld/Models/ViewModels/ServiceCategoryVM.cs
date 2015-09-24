using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelloWorld.Models.ViewModels
{
    public class ServiceCategoryVM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceCategoryVM()
        {
            this.Services = new HashSet<Service>();
            this.ChildrenCategory = new HashSet<ServiceCategoryVM>();
        }

        public int ServiceCategoryId { get; set; }
        public Nullable<int> ServiceCategoryParent { get; set; }
        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Service> Services { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceCategoryVM> ChildrenCategory { get; set; }
        public virtual ServiceCategoryVM ParentCategory { get; set; }
    }


}