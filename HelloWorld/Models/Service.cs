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
    
    public partial class Service
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Service()
        {
            this.ServiceVersions = new HashSet<ServiceVersion>();
            this.Similars = new HashSet<Similar>();
            this.Similars1 = new HashSet<Similar>();
        }
    
        public int ServiceId { get; set; }
        public int ServiceProviderId { get; set; }
        public int ServiceCategoryId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public string SLA { get; set; }
        public string SupportUrl { get; set; }
        public string ServiceCost { get; set; }
        public string ServiceSecurity { get; set; }
        public string ServiceStatus { get; set; }
        public string ServiceType { get; set; }
        public byte[] ServiceLogo { get; set; }
    
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
