//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HelloWorld.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EnterpriseApplication
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EnterpriseApplication()
        {
            this.Workflows = new HashSet<Workflow>();
        }
    
        public int EnterpriseApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public string Specifications { get; set; }
        public byte[] ApplicationLogo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Workflow> Workflows { get; set; }
    }
}
