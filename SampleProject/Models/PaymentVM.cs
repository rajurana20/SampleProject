using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleProject.Models
{
    public class PaymentVM
    {
        public int PId { get; set; }
        public int CId { get; set; }
        public decimal Total { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public System.DateTime PaymentDate { get; set; }
        public System.DateTime ValidDate { get; set; }
        public decimal VAT { get; set; }

        public List<CustomerService>CustomerServices { get; set;}
        public List<Service> Services { get; set; }
        public virtual Customer Customer { get; set; }
    }
}