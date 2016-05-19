using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LandingPage.Models
{
    public class AccountRequest
    {
        public string Company { get; set; }
        public int OrgNr { get; set; }
        public string Email { get; set; }
        public string  Contact { get; set; }
        public double Phone { get; set; }
        public string Message { get; set; }
    }
}