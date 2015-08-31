using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thesis.Models
{
    public class DoctorModel
    {
        public Doctor Doctor { get; set; }
        public Address Address { get; set; }
        public Contact Contact { get; set; }
        public Users Users { get; set; }
        public Role Role { get; set; }
    }

    

    


}