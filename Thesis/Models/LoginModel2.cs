using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thesis.Models
{
    public class LoginModel2
    {
        public Users Users { get; set; }
        public Role Role { get; set; }

        public string CapImage { get; set; }
        //[Required(ErrorMessage = "Varification code is required.")]
        [Compare("CapImageText", ErrorMessage = "Captcha code Invalid")]
        public string CaptchaCodeText { get; set; }
        public string CapImageText { get; set; }
    }
}