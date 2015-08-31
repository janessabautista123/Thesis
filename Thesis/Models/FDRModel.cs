using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Thesis.Models
{
    public class FDRModel
    {
        public FDR FDR { get; set; }
        public Address Address { get; set; }
        public Contact Contact { get; set; }
        public Users Users { get; set; }
        public Role Role { get; set; }
    }
}