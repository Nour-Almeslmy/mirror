using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength]
        public string FirstName { get; set; }

        [MaxLength]
        public string LastName { get; set; }

        public string Role { get; set; }

        //public DateTime DateJoined { get; set; }


    }
}
