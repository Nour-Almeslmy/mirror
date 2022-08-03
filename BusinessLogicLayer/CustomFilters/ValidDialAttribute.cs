using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BusinessLogicLayer.CustomFilters
{
    public class ValidDialAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //string pattern = @"\(?\d{4}\)?-? *\d{3}-? *-?\d{4}";
            string pattern = "^01[0125][0-9]{8}$";
            Regex rg = new Regex(pattern);

            var dial = actionContext.ActionArguments["dial_"] as string;

            if(dial is null || !rg.IsMatch(dial))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            base.OnActionExecuting(actionContext);
        }
    }
}
