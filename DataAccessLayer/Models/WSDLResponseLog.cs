using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Models
{
    public class WSDLResponseLog
    {
        [Key]
        public string dial { get; set; }

        public string MethodName { get; set; }

        public string ResponseLog { get; set; }
    }
}