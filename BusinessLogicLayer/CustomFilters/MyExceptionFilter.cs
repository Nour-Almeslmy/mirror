using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace BusinessLogicLayer.CustomFilters
{
    public class MyExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext Context, CancellationToken cancellationToken)
        {
            Action action = () =>
            {
                Trace.WriteLine("Exception Attribute Capture");

                Context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            };

            var task = new Task(action);
            task.Start();

            return task;
        }
    }
}
