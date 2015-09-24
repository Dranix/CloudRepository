using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelloWorld.Models.ViewModels
{
    public class EnterpriseApplicationVM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EnterpriseApplicationVM()
        {
            this.Workflows = new HashSet<Workflow>();
        }

        public int EnterpriseApplicationId { get; set; }
        [Display(Name = "Application Name")]
        public string ApplicationName { get; set; }
        public string Specifications { get; set; }
        [Display(Name = "Using Services")]
        public ICollection<string> UsingServices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Workflow> Workflows { get; set; }
    }
}