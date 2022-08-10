using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class SignInProviderResponse
    {
        public bool Result { get; private set; }
        public string ErrorMessage { get; private set; }

        public SignInProviderResponse(bool _result, string _errorMessage)
        {
            this.Result = _result;
            this.ErrorMessage = _errorMessage;
        }
    }
}
