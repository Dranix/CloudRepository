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
    
    public partial class Operation
    {
        public int OperationId { get; set; }
        public int ServiceVersionId { get; set; }
        public string OperationName { get; set; }
        public string ServiceParameter { get; set; }
    
        public virtual ServiceVersion ServiceVersion { get; set; }
    }
}