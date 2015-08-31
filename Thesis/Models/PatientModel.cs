using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Thesis.Models
{
    public class PatientModel
    {
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Address Address { get; set; }
        public Contact Contact { get; set; }
        public Users Users { get; set; }
        public Role Role { get; set; }


    }


}