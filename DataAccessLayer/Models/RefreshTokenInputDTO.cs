using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class RefreshTokenInputDTO
    {
        public string Token { get; set; }
        public Guid RefreshToken { get; set; }
    }
}
