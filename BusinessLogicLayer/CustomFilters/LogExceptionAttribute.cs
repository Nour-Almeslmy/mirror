using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;

namespace BusinessLogicLayer.CustomFilters
{
    public class LogExceptionAttribute : ExceptionFilterAttribute
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnException(HttpActionExecutedContext context)
        {
            //Context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);

            log.Warn($"{context.ActionContext.ActionDescriptor.ActionName} / " + 
                    $"{context.ActionContext.ControllerContext.ControllerDescriptor.ControllerName} / "+
                    $"{context.Exception.Message}" 
                    );


        }
    }
}
