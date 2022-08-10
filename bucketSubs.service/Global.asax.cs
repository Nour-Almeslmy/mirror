using System.Web.Http;

namespace bucketSubs.service
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            UnityWebApiActivator.Start();
        }
    }
}
