//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SampleProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomerService
    {
        public int CSID { get; set; }
        public int CId { get; set; }
        public int SId { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Service Service { get; set; }
    }
}
